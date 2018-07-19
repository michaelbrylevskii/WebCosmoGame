function addLog(message) {
    document.body.innerHTML += "<br>" + new Date().toString() + ": " + message;
}
addLog("Подключение...");
var socket = new WebSocket("ws://localhost:5000/connect");
socket.onopen = function () {
    addLog("Соединение установлено");
};
socket.onclose = function (event) {
    if (event.wasClean) {
        addLog('Соединение закрыто чисто');
    }
    else {
        addLog('Обрыв соединения');
    }
    addLog('Код: ' + event.code + ' причина: ' + event.reason);
};
socket.onmessage = function (event) {
    addLog("Получены данные \"" + event.data + "\"");
};
socket.onerror = function (error) {
    addLog("Ошибка " + error.message);
};
function socketSend() {
    socket.send("Привет");
}
