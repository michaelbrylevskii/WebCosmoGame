function addLog(message:String) {
    document.body.innerHTML +=  "<br>" + new Date().toString() + ": " + message;
}

addLog("Подключение...");
var socket = new WebSocket("ws://localhost:5000/connect");

socket.onopen = function() {
    addLog("Соединение установлено");
};

socket.onclose = function(event:CloseEvent) {
    if (event.wasClean) {
        addLog('Соединение закрыто чисто');
    } else {
        addLog('Обрыв соединения');
    }
    addLog('Код: ' + event.code + ' причина: ' + event.reason);
};

socket.onmessage = function(event:MessageEvent) {
    addLog("Получены данные \"" + event.data + "\"");
};

socket.onerror = function(error:ErrorEvent) {
    addLog("Ошибка " + error.message);
};

function socketSend() {
    socket.send("Привет");
}