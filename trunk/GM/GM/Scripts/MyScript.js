var map;
var marker;
var geocoder;
var option;
var infowindow;
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
    marker = new google.maps.Marker({ map: map });

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
            if (!marker) {
                marker = new google.maps.Marker({ map: map });
            }
            marker.setPosition(results[0].geometry.location);
            if (!infowindow) {
                infowindow = new google.maps.InfoWindow();
            }
            document.getElementById("address").value = results[0].formatted_address;
            latitude = results[0].geometry.location.lat();
            longitude = results[0].geometry.location.lng();

            var content = '<strong>' + results[0].formatted_address + '</strong></br>';
            content += 'Latitude:' + results[0].geometry.location.lat() + '</br>';
            content += 'Longitude:' + results[0].geometry.location.lng() + '</br></br></br>';
            infowindow.setContent(content);
            infowindow.open(map, marker);
            if (flag == true) {
                var panel = document.getElementById("listaddress");
                var panelContent = "<strong>Similar address:</strong></br>";
                for (var i in results) {
                    panelContent += "<a href='javascript:void(0);' name='" + results[i].formatted_address + "' onclick=linkAddress_Click(this)>" + results[i].formatted_address + "<a/>" + "</br></br>";
                }
                panel.innerHTML = panelContent;
            }
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        }
        else {
            alert('address not found!');
        }
    });
}

function addMarker() {
    var center = map.getCenter();
    add(center);
}

function add(point) {
    marker = new google.maps.Marker({
        position: point, 
        map: map,
        draggable: true
    });

    google.maps.event.addListener(marker, 'dblclick', function() {
        var hdnLat = document.getElementById("hdnLat");
        var hdnLng = document.getElementById("hdnLng");

        hdnLat.value = this.getLatLng().lat();
        hdnLng.value = this.getLatLng().lng();

        var arg = this.getLatLng().lng().toString() + ";" + this.getLatLng().lat().toString();
        var context = "";
        CallServer(arg, context);
    });
}

function ReceiveServerData(rValue) {
    if (rValue) {
        marker.openInfoWindow("New position has been added!");
    }
}

function OnAddLocationComplete() {
}

function deleteAllMarker() {
    map.clearOverlays();
}