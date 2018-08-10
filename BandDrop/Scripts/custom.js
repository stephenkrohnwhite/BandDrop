function append_message(user_name, message) {

    return '<li class="right clearfix">' +
        '<div class="chat-body clearfix">' +
        '<div class="header">' +
        '<strong class="pull-left primary-font">' + user_name + '</strong><br>' +
        '</div>' +
        '<p>' +
        message +
        '</p>' +
        '</div>' +
        '</li>';
}
var pusher = new Pusher("de9851a0744ee8227c97", {
    cluster: "us2", //PUSHER APP CLUSTER
    encrypted: true
});

var channel = pusher.subscribe('dotNetGroupChat');

channel.bind('asp-event', function (data) {

    $("#chat-body").append(append_message(data.userName, data.message));
});

$("#btn-chat").click(function () {

    $.post("/Message/Create", { message: $("#btn-input").val() });

    $("#btn-input").val('');

})