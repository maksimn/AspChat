// Реализация прекращения пользователем сессии (при закрытии вкладки браузера)
window.onunload = function () {
    function deleteCookie() {
        document.cookie = "id=; expires=Thu, 01 Jan 1970 00:00:00 UTC";
    }
    deleteCookie();
}
