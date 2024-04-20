const BACKEND_URL = 'https://localhost:7108/api'
const AUTHENTICATION_CONTROLLER = `${BACKEND_URL}/Authentication`
const USER_CONTROLLER = `${BACKEND_URL}/User`
const POST_CONTROLLER = `${BACKEND_URL}/Post`
const COMMENT_CONTROLLER = `${BACKEND_URL}/Comment`
const REPORT_CONTROLLER = `${BACKEND_URL}/Report`
const FEEDBACK_CONTROLLER = `${BACKEND_URL}/Feedback`

const USERNAME_RESTRICTIONS = /[^A-Za-z0-9 ]/g;

export
    {
        BACKEND_URL,
        AUTHENTICATION_CONTROLLER,
        POST_CONTROLLER, USER_CONTROLLER,
        COMMENT_CONTROLLER,
        USERNAME_RESTRICTIONS,
        REPORT_CONTROLLER,
        FEEDBACK_CONTROLLER
    };