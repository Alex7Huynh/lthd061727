var map;
var geocoder;
var option;
var latitude;
var longitude;
var browserSupportFlag;
var initLocation;
var vietnam;

function initialize() {
    vietnam = new google.maps.LatLng(14.058324, 108.277199);
    var myLatlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
        zoom: 8,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map"),
        myOptions);
    geocoder = new google.maps.Geocoder();

    // Try W3C Geolocation (Preferred)
    if (navigator.geolocation) {
        browserSupportFlag = true;
        navigator.geolocation.getCurrentPosition(function (position) {
            initLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(initLocation);
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;
        }, function () {
            handleNoGeolocation(browserSupportFlag);
        });
    }

    // Try Google Gears Geolocation
    else if (google.gears) {
        browserSupportFlag = true;
        var geo = google.gears.factory.create('beta.geolocation');
        geo.getCurrentPosition(function (position) {
            initLocation = new google.maps.LatLng(position.latitude, position.longitude);
            map.setCenter(initLocation);
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;
        }, function () {
            handleNoGeolocation(browserSupportFlag);
        });
    }

    // Browser doesn't support Geolocation
    else {
        browserSupportFlag = false;
        handleNoGeolocation(browserSupportFlag);
    }
}

function handleNoGeolocation(errorFlag) {
    if (errorFlag == true) {
        alert("Geolocation hast return an error!");
        initLocation = vietnam;
    }
    else {
        alert("Your browser doesn't suport geolocation!");
        initLocation = vietnam;
    }
    map.setCenter(initLocation);
}


function showAddress() {
    var address = document.getElementById("address").value;
    findLocation(address, true);
}

function linkAddress_Click(link) {
    var address = link.name;
    findLocation(address, false);
}

function findLocation(address, flag) {
    if (!geocoder) {
        geocoder = new google.maps.Geocoder();
    }
    var geocoderRequest = { address: address };
    geocoder.geocode(geocoderRequest, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            add(results[0].formatted_address, results[0].geometry.location);

            document.getElementById("address").value = results[0].formatted_address;

            if (flag == true) {
                var panel = document.getElementById("listaddress");
                var panelContent = "<strong>Similar address:</strong></br>";
                for (var i in results) {
                    panelContent += "<a href='javascript:void(0);' name='" + results[i].formatted_address + "' onclick=linkAddress_Click(this)>" + results[i].formatted_address + "<a/>" + "</br></br>";
                }
                panel.innerHTML = panelContent;
            }
        }
        else {
            alert('address not found!');
        }
    });
}

function addLocation(address, lat, lng) {
    var arg = "add" + ";" + address + ";" + lng + ";" + lat;
    var context = "";
    CallServer(arg, context);
}

function removeLocation(address, lat, lng) {
    var arg = "remove" + ";" + address + ";" + lng + ";" + lat;
    var context = "";
    CallServer(arg, context);
}

function updateLocation(address, lat, lng) {
    var arg = "update" + ";" + address + ";" + lng + ";" + lat;
    var context = "";
    CallServer(arg, context);
}

function addMarker() {
    var center = map.getCenter();
    add("", center);
}

function add(address, point) {
    var marker = new google.maps.Marker({ position: point, map: map, draggable: true });

    
    var infowindow = new google.maps.InfoWindow();

    var content = '<strong>' + address + '</strong></br>';
    content += 'Latitude:' + point.lat() + '</br>';
    content += 'Longitude:' + point.lng() + '</br></br>';
    content += '<a href="javascript:void(null);" onclick="addLocation(' + address + ',' + point.lng() + ',' + point.lat() + ');">Add this location to your list</a></br>';
    content += '<a href="javascript:void(null);" onclick="removeLocation(' + address + ',' + point.lng() + ',' + point.lat() + ');">remove this location from your list</a></br>';
    content += '<a href="javascript:void(null);" onclick="updateLocation(' + address + ',' + point.lng() + ',' + point.lat() + ');">Update position for this location</a>';
    infowindow.setContent(content);
    infowindow.open(map, marker);

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });
}

function ReceiveServerData(rValue) {
    if (rValue) {
    }
}

function OnAddLocationComplete() {
}

function deleteAllMarker() {
    map.clearOverlays();
}