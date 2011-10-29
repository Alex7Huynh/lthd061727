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
            //document.getElementById("viDo").value = results[0].geometry.location.lat();
            //document.getElementById("kinhDo").value = results[0].geometry.location.lng();

            var content = "<strong><input id='TenDiaDiem' type=text  value='" + results[0].formatted_address + "' /></strong><br/>";
            content += "Vĩ độ: <input id='ViDo' type=text readonly='readonly' value='" + results[0].geometry.location.lat() + "' /><br/>";
            content += "Kinh độ: <input id='KinhDo' type=text readonly='readonly' value='" + results[0].geometry.location.lng() + "' /><br/>";
            content += "Danh mục: <input id='TenDanhMuc' type=text /><br/>";
            content += "Ghi chú: <input id='GhiChu' type=text /><br/><br/><br/>";
            content += "<a href='javascript:void(0);' name='" + results[0].formatted_address + "' onclick=themDiaDiem()>Thêm</a>";

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


function findMyLocation(idAddress, address, note) {
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
            //document.getElementById("viDo").value = results[0].geometry.location.lat();
            //document.getElementById("kinhDo").value = results[0].geometry.location.lng();


            var content = "<input id='MaDiaDiem' type=hidden value='" + idAddress + "' /><br/>";
            content += "<strong><input id='TenDiaDiem' type=text  value='" + results[0].formatted_address + "' /></strong><br/>";
            content += "Vĩ độ: <input id='ViDo' type=text readonly='readonly' value='" + results[0].geometry.location.lat() + "' /><br/>";
            content += "Kinh độ: <input id='KinhDo' type=text readonly='readonly' value='" + results[0].geometry.location.lng() + "' /><br/>";
            content += "Ghi chú: <input id='GhiChu' type=text value='" + note + "' /><br/><br/><br/>";
            content += "<a href='javascript:void(0);' name='" + results[0].formatted_address + "' onclick=xoaDiaDiem()>Xóa</a>&nbsp&nbsp";
            content += "<a href='javascript:void(0);' name='" + results[0].formatted_address + "' onclick=capNhatDiaDiem()>Cập nhật</a>";

            infowindow.setContent(content);
            infowindow.open(map, marker);
            /*
            if (flag == true) {
            var panel = document.getElementById("diadiempanel");
            var panelContent = "<strong>Các kết quả tìm được:</strong></br>";
            for (var i in results) {
            panelContent += "<a href='javascript:void(0);' name='" + results[i].formatted_address + "' onclick=linkDiaDiem_Click(this)>" + results[i].formatted_address + "</a>" + "</br></br>";
            }
            panel.innerHTML = panelContent;
            }*/

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        }
        else {
            alert('Không tìm thấy địa chỉ cần tìm');
        }
    });
}

//Them dia diem
function themDiaDiem() {
    //Lay du lieu     
    var tenDiaDiem = $get("TenDiaDiem").value;
    var viDo = $get("ViDo").value;
    var kinhDo = $get("KinhDo").value;
    var tenDanhMuc = $get("TenDanhMuc").value;
    var ghiChu = $get("GhiChu").value;
    //Cap nhat treeview
    var tmp = $get("TenDiaDiem").value;
    //Them dia diem
    PageMethods.ThemDiaDiem(tenDiaDiem, viDo, kinhDo, ghiChu, tenDanhMuc, OnCallThemDiaDiemComplete, OnFailed);
}

//Xoa dia diem
function xoaDiaDiem() {
    //Lay du lieu 
    var maDiaDiem = $get("MaDiaDiem").value;
    var tenDiaDiem = $get("TenDiaDiem").value;
    //Cap nhat treeview
    var parent = document.getElementById("CayDiaDiem");    
    var tmp = $get("DD" + maDiaDiem);
    parent.removeChild(tmp);
    //Xoa dia diem
    PageMethods.XoaDiaDiem(maDiaDiem, OnCallXoaDiaDiemComplete, OnFailed);
}

//Cap nhat dia diem
function capNhatDiaDiem() {
    //Lay du lieu 
    var maDiaDiem = $get("MaDiaDiem").value;
    var tenDiaDiem = $get("TenDiaDiem").value;
    var viDo = $get("ViDo").value;
    var kinhDo = $get("KinhDo").value;
    var ghiChu = $get("GhiChu").value;
    //Cap nhat treeview
    var parent = document.getElementById("CayDiaDiem");
    var tmp = $get("DD" + maDiaDiem);
    $get("DD" + maDiaDiem).href = "javascript:(findMyLocation('" + maDiaDiem + ", '" + tenDiaDiem + "', " + ghiChu + "'))";
    $get("DD" + maDiaDiem).innerHTML = "&nbsp;&nbsp;+&nbsp;" + tenDiaDiem + "<br/>";
    
    //tmp.id= "DD1"
    //tmp.href =     "javascript:(findMyLocation('1', 'Trường ĐH Khoa học Tự nhiên, 227 Nguyễn Văn Cừ, phường 4, Quận 5, Hồ Chí Minh, Việt Nam', 'cool'))"
    //tmp.pathname = "(findMyLocation('1', 'Trường ĐH Khoa học Tự nhiên, 227 Nguyễn Văn Cừ, phường 4, Quận 5, Hồ Chí Minh, Việt Nam', 'cool'))"
    //tmp.nameProp = "javascript:(findMyLocation('1', 'Trường ĐH Khoa học Tự nhiên, 227 Nguyễn Văn Cừ, phường 4, Quận 5, Hồ Chí Minh, Việt Nam', 'cool'))"
    //innerHTML = "&nbsp;&nbsp;+&nbsp;Trường ĐH Khoa học Tự nhiên, 227 Nguyễn Văn Cừ, phường 4, Quận 5, Hồ Chí Minh, Việt Nam<br>"

    //Cap nhat dia diem
    PageMethods.CapNhatDiaDiem(maDiaDiem, tenDiaDiem, viDo, kinhDo, ghiChu, OnCallCapNhatDiaDiemComplete, OnFailed);
}

//Hoan thanh them dia diem
function OnCallThemDiaDiemComplete() {
    alert('Thêm thành công!');
}

//Hoan thanh xoa dia diem
function OnCallXoaDiaDiemComplete() {
    alert('Xóa thành công!');
}

//Hoan thanh cap nhat dia diem
function OnCallCapNhatDiaDiemComplete() {
    alert('Cập nhật thành công!');
}
function OnFailed(error) {
    if (error !== null) {
        alert(error.get_message());
    }
}