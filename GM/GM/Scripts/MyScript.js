var map;
var geocoder;
var option;
var latitude;
var longitude;
var browserSupportFlag;
var initLocation;
var vietnam;
var menuMap;
var menuMark;
var current;
var overlay;
var $map;

/**************************************************
*                      Menu
**************************************************/

function Menu() {
    var that = this,
            ts = null;
    $map = $('#map');
    this.items = [];

    // create an item using a new closure 
    this.create = function (item) {
        var $item = $('<div class="item ' + item.cl + '">' + item.label + '</div>');
        $item
        // bind click on item
            .click(function () {
                if (typeof (item.fnc) === 'function') {
                    item.fnc.apply($(this), []);
                }
            })
        // manage mouse over coloration
            .hover(
              function () { $(this).addClass('hover'); },
              function () { $(this).removeClass('hover'); }
            );
        return $item;
    };
    this.clearTs = function () {
        if (ts) {
            clearTimeout(ts);
            ts = null;
        }
    };
    this.initTs = function (t) {
        ts = setTimeout(function () { that.close() }, t);
    };
}

// add item
Menu.prototype.add = function (label, cl, fnc) {
    this.items.push({
        label: label,
        fnc: fnc,
        cl: cl
    });
}

// close previous and open a new menu
Menu.prototype.open = function (event) {
    //this.close();
    var k,
            that = this,
            offset = {
                x: 0,
                y: 0
            },
            $menu = $('<div id="menu"></div>');

    // add items in menu
    for (k in this.items) {
        $menu.append(this.create(this.items[k]));
    }

    // manage auto-close menu on mouse hover / out
    $menu.hover(
          function () { that.clearTs(); },
          function () { that.initTs(3000); }
        );

    var containerPixel = overlay.getProjection().fromLatLngToContainerPixel(event.latLng);
    // change the offset to get the menu visible (#menu width & height must be defined in CSS to use this simple code)
    if (containerPixel.y + $menu.height() > $map.height()) {
        offset.y = -$menu.height();
    }
    if (containerPixel.x + $menu.width() > $map.width()) {
        offset.x = -$menu.width();
    }

    // use menu as overlay
    $map.gmap3({
        action: 'addOverlay',
        latLng: event.latLng,
        content: $menu,
        offset: offset
    });

    // start auto-close
    this.initTs(5000);
}

// close the menu
Menu.prototype.close = function () {
    this.clearTs();
    $map.gmap3({ action: 'clear', name: 'overlay' });
}

/**************************************************
*                      Main
**************************************************/

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

    overlay = new google.maps.OverlayView();
    overlay.draw = function () { };
    overlay.setMap(map);

    menuMap = new Menu();

    // MENU : ITEM 1
    menuMap.add('Add To My Location', 'addToMyLocation',
          function () {
              //menuMap.close();
              //addLocation(false);
          });

    // MENU : ITEM 2
    menuMap.add('Add Mark Here', 'addMarkHere',
          function () {
              //menuMap.close();
              //addMarker(true);
          })

    // MENU : ITEM 3
    menuMap.add('Delete This Mark', 'deleteThisMark',
          function () {
              //map.setZoom(map.getZoom() + 1);
              //menuMap.close();
          });

    // MENU : ITEM 4
    menuMap.add('Delete This Location', 'deleteThisLocation',
          function () {
              //map.setZoom(map.getZoom() - 1);
              //menu.close();
          });

    // MENU : ITEM 5
    menuMap.add('Update Movement', 'updateMovement',
          function () {
              //map.setCenter(current.latLng);
              //menuMap.close();
          });

    // MENU : ITEM 6
    menuMap.add('Cancel Movement', 'cancelMovement',
          function () {
              //map.setCenter(current.latLng);
              //menuMap.close();
          });

    // MENU : ITEM 7
    menuMap.add('Rename', 'rename',
        function () {
            //map.setCenter(current.latLng);
            //menuMap.close();
        });

    google.maps.event.addListener(map, 'rightclick', function (event) {
        current = event;
        menuMap.open(current);
    });
    google.maps.event.addListener(map, 'click', function (event) {
        menuMap.close();
    });
    google.maps.event.addListener(map, 'dragstart', function () {
        menuMap.close();
    });
    google.maps.event.addListener(map, 'zoom_changed', function () {
        menuMap.close();
    });
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