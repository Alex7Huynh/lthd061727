/// <reference path="google-maps-3-vs-1-0.js" />
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
                        
            var content = "<strong><input type=text  value='" + results[0].formatted_address + "' /></strong><br/>";
            content += 'Vĩ độ: ' + results[0].geometry.location.lat() + '<br/>';
            content += 'Kinh độ: ' + results[0].geometry.location.lng() + '<br/>';
            content += 'Ghi chú: <input type=text /><br/><br/><br/>';
            //            content += '<a href="ThemDiaDiem.aspx?action=Them&ten=' + results[0].formatted_address
            //                + '&lat=' + results[0].geometry.location.lat()
            //                + '&lgn=' + results[0].geometry.location.lng() + '">Thêm</a>&nbsp&nbsp';            
            content += "<a href='javascript:void(0);' name='" + results[0].formatted_address + "' onclick=themDiaDiem(this)>Thêm</a>";


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


function findMyLocation(address, flag, note) {
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
            document.getElementById("DiaDiem").value = results[0].formatted_address;
            document.getElementById("viDo").value = results[0].geometry.location.lat();
            document.getElementById("kinhDo").value = results[0].geometry.location.lng();

            var content = '<strong>' + results[0].formatted_address + '</strong><br/>';
            content += 'Ghi chú: ' + note + '<br/>';
            content += 'Vĩ độ: ' + results[0].geometry.location.lat() + '<br/>';
            content += 'Kinh độ: ' + results[0].geometry.location.lng() + '<br/><br/><br/>';
            content += '<a href="index.aspx?action=Xoa&ten=' + results[0].formatted_address + '">Xóa</a>&nbsp&nbsp';
            content += '<a href="CapNhatDiaDiem.aspx?action=CapNhat&ten=' + results[0].formatted_address + '">Cập nhật</a>';
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
        }
        else {
            alert('Không tìm thấy địa chỉ cần tìm');
        }
    });
}

function themDiaDiem(ten) {
    alert(ten.name);
    var txt1 = $get("viDo");
    var txt2 = $get("kinhDo");
    var txtresult = $get("DiaDiem");

    //Call server side function
    PageMethods.Sum(txt1.value, txt2.value, OnCallSumComplete, OnCallSumError, txtresult);
}
function xoaDiaDiem() {

}
function capNhatDiaDiem() {

}
function OnCallSumComplete(result, txtresult, methodName) {
    //Show the result in txtresult
    txtresult.value = result;
}
function OnCallSumError(error, userContext, methodName) {
    if (error !== null) {
        alert(error.get_message());
    }
}