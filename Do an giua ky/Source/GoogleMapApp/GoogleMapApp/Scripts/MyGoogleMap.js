﻿/// <reference path="google-maps-3-vs-1-0.js" />
var map;
var vietnam = new google.maps.LatLng(14.058324, 108.277199);
var initLocation;
var browserSupportFlag;
var geocoder;
var marker;
var infowindow;
var latitude;
var longitude;
var clickedPixel;
var contextmenu;


//ham xu ly khong dinh vi duoc
function handleNoGeolocation(errorFlag) {
    if (errorFlag == true) {
        alert("Dịch vụ định vị địa lý có lỗi!");
        initLocation = vietnam;
    }
    else {
        alert("Trình duyệt không hỗ trợ định vị địa lý!");
        initLocation = vietnam;
    }
    map.setCenter(initLocation);
}

//ham khoi tao ban do
function initialize() {
    var myOptions = {
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map"), myOptions);

    /*contextmenu = document.createElement("div");
    contextmenu.style.visibility = "hidden";
    contextmenu.style.background = "#ffffff";
    contextmenu.style.border = "1px solid #8888FF";

    contextmenu.innerHTML = '<a href="zoomIn()"><div class="context">&nbsp;&nbsp;Zoom in&nbsp;&nbsp;<\/div><\/a>'
                            + '<a href="zoomOut()"><div class="context">&nbsp;&nbsp;Zoom out&nbsp;&nbsp;<\/div><\/a>'
                            + '<a href="zoomInHere()"><div class="context">&nbsp;&nbsp;Zoom in here&nbsp;&nbsp;<\/div><\/a>'
                            + '<a href="zoomOutHere()"><div class="context">&nbsp;&nbsp;Zoom out here&nbsp;&nbsp;<\/div><\/a>'
                            + '<a href="centreMapHere()"><div class="context">&nbsp;&nbsp;Centre map here&nbsp;&nbsp;<\/div><\/a>';
                            */
    

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

    //map.getDiv().appendChild(contextmenu);
}

//Tim dia diem
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
            //marker.setPosition(LatLng(10.5, 106.5, true));
            //marker = new google.maps.Marker({ position: new google.maps.LatLng(40.756054, -73.986951), map: map });
            if (!infowindow) {
                infowindow = new google.maps.InfoWindow();
            }
            document.getElementById("DiaDiem").value = results[0].formatted_address;
            document.getElementById("viDo").value = results[0].geometry.location.lat();
            document.getElementById("kinhDo").value = results[0].geometry.location.lng();

            var content = '<strong>' + results[0].formatted_address + '</strong></br>';
            content += 'Vĩ độ:' + results[0].geometry.location.lat() + '</br>';
            content += 'Kinh độ:' + results[0].geometry.location.lng() + '</br></br></br>';
            content += '<a href="index.aspx?action=Them&ten=' + results[0].formatted_address
                + '&lat=' + results[0].geometry.location.lat()
                + '&lgn=' + results[0].geometry.location.lng() + '">Thêm</a>&nbsp&nbsp';
            content += '<a href="index.aspx?action=Xoa">Xóa</a>&nbsp&nbsp';            
            infowindow.setContent(content);
            infowindow.open(map, marker);
            if (flag == true) {
                var panel = document.getElementById("diadiempanel");
                var panelContent = "<strong>Các kết quả tìm được:</strong></br>";
                for (var i in results) {
                    panelContent += "<a href='javascript:void(0);' name='" + results[i].formatted_address + "' onclick=linkDiaDiem_Click(this)>" + results[i].formatted_address + "</a>" + "</br></br>";
                }
                panel.innerHTML = panelContent;
            }
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
            /*google.maps.event.addListener(marker, 'singlerightclick', function (pixel, tile) {
            clickedPixel = pixel;
            var x = pixel.x;
            var y = pixel.y;
            if (x > map.getSize().width - 120) { x = map.getSize().width - 120 }
            if (y > map.getSize().height - 100) { y = map.getSize().height - 100 }
            var pos = new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(x, y));
            pos.apply(contextmenu);
            contextmenu.style.visibility = "visible";
            });*/
        }
        else {
            alert('Không tìm thấy địa chỉ cần tìm');
        }
    });
}

//ham xu ly su kien button click
function btnDiaDiem_Click() {
    var address = document.getElementById("DiaDiem").value;
    findLocation(address, true);
}

//ham xu ly su kien link click
function linkDiaDiem_Click(link) {
    var DiaDiem = link.name;
    findLocation(DiaDiem, false);
}

//ham xu ly su kien button click
function btnDiaDiemMoi_Click() {
    var x = document.getElementById("viDo").value;
    var y = document.getElementById("kinhDo").value;
    //    //findLocation(viDo, kinhDo, true);    
    alert(x.toString() + " - " + y.toString());    
}

//ham xu ly su kien button click
function btnMyLocation_Click() {
    map.setZoom(20);
    var content = 'Vi tri cua ban</br>' + 'Vi do: ' + latitude + '</br> kinh do: ' + longitude;
    var marker = new google.maps.Marker({
        position: initLocation,
        map: map
    });
    // Creating a InfoWindow
    var infoWindow = new google.maps.InfoWindow({
        content: content
    });
    // Adding the InfoWindow to the map
    infoWindow.open(map, marker);

}

////Tim dia diem theo vi do kinh do
//function findLocation(viDo, kinhDo, flag) {
//    if (!geocoder) {
//        geocoder = new google.maps.Geocoder();
//    }
//    var geocoderRequest = { address: address };
//    geocoder.geocode(geocoderRequest, function (results, status) {
//        if (status == google.maps.GeocoderStatus.OK) {
//            if (!marker) {
//                marker = new google.maps.Marker({ map: map });
//            }
//            marker.setPosition(results[0].geometry.location);
//            if (!infowindow) {
//                infowindow = new google.maps.InfoWindow();
//            }
//            var content = '<strong>' + results[0].formatted_address + '</strong></br>';
//            content += 'Vĩ độ:' + results[0].geometry.location.lat() + '</br>';
//            content += 'Kinh độ:' + results[0].geometry.location.lng() + '</br></br></br>';
//            infowindow.setContent(content);
//            infowindow.open(map, marker);
//            if (flag == true) {
//                var panel = document.getElementById("diadiempanel");
//                var panelContent = "<strong>Các kết quả tìm được:</strong></br>";
//                for (var i in results) {
//                    panelContent += "<a href='javascript:void(0);' name='" + results[i].formatted_address + "' onclick=linkDiaDiem_Click(this)>" + results[i].formatted_address + "</a>" + "</br></br>";
//                }
//                panel.innerHTML = panelContent;
//            }
//            google.maps.event.addListener(marker, 'rightclick', function () {
//                infowindow.open(map, marker);
//            });
//        }
//        else {
//            alert('Không tìm thấy địa chỉ cần tìm');
//        }
//    });
//}

function zoomIn() {    
    map.zoomIn();    
    contextmenu.style.visibility = "hidden";
}
function zoomOut() {
    map.zoomOut();
    contextmenu.style.visibility = "hidden";
}
function zoomInHere() {
    var point = map.fromContainerPixelToLatLng(clickedPixel)
    map.zoomIn(point, true);
    contextmenu.style.visibility = "hidden";
}
function zoomOutHere() {
    var point = map.fromContainerPixelToLatLng(clickedPixel)
    map.setCenter(point, map.getZoom() - 1); // There is no map.zoomOut() equivalent
    contextmenu.style.visibility = "hidden";
}
function centreMapHere() {
    var point = map.fromContainerPixelToLatLng(clickedPixel)
    map.setCenter(point);
    contextmenu.style.visibility = "hidden";
}
/*function setMarker(lat, lgn) {
    marker = new google.maps.Marker({ position: new google.maps.LatLng(lat, lgn), map: map });
}*/
function setMarker() {
    var x = parseFloat(document.getElementById("viDo").value);
    var y = parseFloat(document.getElementById("kinhDo").value);
    alert(x.toString() + " - " + y.toString());
    marker = new google.maps.Marker({ position: new google.maps.LatLng(x, y), map: map });
}

function myFunc() {
    var x = document.getElementById("TreeView1").value;
    alert(x.toString());
}