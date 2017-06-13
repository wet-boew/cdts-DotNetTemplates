namespace WebTemplateCore.Proxies
{
    public interface ISessionTimeOutElementProxy
    {
        /// <summary>
        /// enabled
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// inactivity period of time after which the modal dialog will appear (default 20 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        int Inactivity { get; }

        /// <summary>
        /// period of time the user has to perform an action once the modal dialog is displayed (default 3 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        int ReactionTime { get; }

        /// <summary>
        /// period of time for the session to stay alive until the modal dialog appears (default 20 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        int Sessionalive { get; }

        /// <summary>
        /// URL that users are sent to when the session has expired.
        /// </summary>
        string Logouturl { get; }

        /// <summary>
        /// URL used to perform an ajax request to determine the validity of the session.
        /// </summary>
        string RefreshCallbackUrl { get; }

        /// <summary>
        /// Determines if clicking on the document should reset the inactivity timeout and perform an ajax request (if a refreshCallbackUrl has been specified).
        /// </summary>
        bool RefreshOnClick { get; }

        /// <summary>
        /// Sets the amount of time that must pass before an ajax request can be made.
        /// </summary>
        int RefreshLimit { get; }

        /// <summary>
        /// Sets the request method used for ajax requests. Recommended: GET or POST.
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Additional data to send with the request.
        /// </summary>
        string AdditionalData { get; }
    }
}