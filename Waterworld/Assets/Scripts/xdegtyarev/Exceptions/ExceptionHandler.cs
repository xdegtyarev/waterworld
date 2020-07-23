using UnityEngine;

public static class ExceptionHandler {
    public static event System.Action OnError;
    static string prevExceptionLogString = "";
    static string prevExceptionStrackTrace = "";
    public static void Init() {
        prevExceptionLogString = "";
        prevExceptionStrackTrace = "";
        System.AppDomain.CurrentDomain.UnhandledException += _OnUnresolvedExceptionHandler;
        Application.logMessageReceived += _OnDebugLogCallbackHandler;
    }

    public static void Disable(){
        System.AppDomain.CurrentDomain.UnhandledException -= _OnUnresolvedExceptionHandler;
        Application.logMessageReceived -= _OnDebugLogCallbackHandler;
    }

    static private void _OnUnresolvedExceptionHandler(object sender, System.UnhandledExceptionEventArgs args) {
        Error();
    }

    static void Error() {
        if (OnError != null) {  
            OnError();
            OnError = null;
        }
        System.AppDomain.CurrentDomain.UnhandledException -= _OnUnresolvedExceptionHandler;
        Application.logMessageReceived -= _OnDebugLogCallbackHandler;
    }
 
    static private void _OnDebugLogCallbackHandler(string logEntry, string stackTrace, LogType logType) {
        if (logType == LogType.Log || logType == LogType.Warning)
            return; 
        if (logType == LogType.Error || logType == LogType.Exception || logType == LogType.Assert) {
            #if !UNITY_EDITOR
            Analytics.gua.cancelHit();
            #endif
            if (!prevExceptionLogString.Equals(logEntry) || !prevExceptionStrackTrace.Equals(stackTrace)) {
                #if !UNITY_EDITOR
                Analytics.gua.sendExceptionHit("(" + logType + ")" + logEntry + ":\n" + stackTrace, logType != LogType.Error);
                #endif
                Error();
            }
            prevExceptionLogString = logEntry ?? "";
            prevExceptionStrackTrace = stackTrace ?? "";
        }
    }
}
