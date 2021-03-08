
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $("#getLocation").click(function () {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                alert('Geolocation is not supported by this browser.');
            }
    })
});

function showPosition(position) {
    $("#Latitude").val(position.coords.latitude);
    $("#Longitude").val(position.coords.longitude);
};

