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

var homesCollection = $("#allHomes > p");
var markers = [];
var positions = [];
var spans = [];

function initMap() {
    const Iasi = { lat: 47.1585, lng: 27.6014 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 14,
        center: Iasi,
    });
    var geocoders = new google.maps.Geocoder();
    for (let i = 0; i < homesCollection.length; i++) {
        spans[i] = homesCollection[i].getElementsByTagName('span');
        if (spans[i][3].innerHTML == null || spans[i][3].innerHTML == "") {//if coordinates are missing
            geocoders.geocode({ address: spans[i][5].innerHTML }, (results, status) => {
                if (status === "OK") {
                    markers[i] = new google.maps.Marker({
                        position: results[0].geometry.location,
                        title: spans[i][1].innerHTML + " Grade: " + spans[i][2].innerHTML + " Price: " + spans[i][6].innerHTML,
                        map: map,
                    });
                    markers[i].addListener("click", () => { window.open("ReviewedHome/Details/" + spans[i][0].innerHTML); });
                } else {
                    alert("Something went wrong with geocoding")
                }
            });
        } else {
            positions[i] = { lat: parseFloat(spans[i][3].innerHTML), lng: parseFloat(spans[i][4].innerHTML) };
            markers[i] = new google.maps.Marker({
                position: positions[i],
                title: spans[i][1].innerHTML + " Grade: " + spans[i][2].innerHTML + " Price: " + spans[i][6].innerHTML,
                map: map,
            });
            markers[i].addListener("click", () => { window.open("ReviewedHome/Details/" + spans[i][0].innerHTML); });
        }
    }
};



