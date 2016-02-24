$(document).ready(function () {
    initMap();
});

function initMap() {
    var latitude = Number($("#Latitude").val());
    var longitude = Number($("#Longitude").val());
    var name = $("#Name").val();
    var address = $("#Address").val();
    var imgPath = $("#CommunityImage_UrlPath").val().slice(1);
    var imgName = $("#CommunityImage_NewFileName").val();

    var Sofia = { lat: latitude, lng: longitude };

    var map = new google.maps.Map(document.getElementById('map_canvas'), {
        center: Sofia,
        zoom: 18,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var marker = new google.maps.Marker({
        map: map,
        position: Sofia
    });

    

    var infowindow = new google.maps.InfoWindow({
        maxWidth: 200,
        content: '<div id="iw-container">' +
                    '<div class="iw-title"><strong>' + name +'</strong></div>' +
                    '<div class="iw-content">' +
                      '<div class="iw-subTitle">' + address + '</div>' +
                      '<img src="' +imgPath+'/'+imgName+ '" alt="Porcelain Factory of Vista Alegre" width="100">' +
                    '</div>' +
                    '<div class="iw-bottom-gradient"></div>' +
                  '</div>'

    });

    infowindow.open(map, marker)

    marker.addListener('click', function () {
        infowindow.open(map, marker)
    });
}

function initInfowindow() {

    var infowindow = new google.maps.InfoWindow({
        maxWidth: 150
    });

    infowindow.open(map, marker)

    return infowindow;
}

function geocodePosition(pos, infowindow) {

    var findResult = function (results, name) {
        var result = _.find(results, function (obj) {
            return obj.types[0] == name && obj.types[1] == "political";
        });
        return result ? result.short_name : null;
    };
    geocoder = new google.maps.Geocoder();

    geocoder.geocode({
        latLng: pos
    }, function (responses) {
        if (responses && responses.length > 0) {
            $('#Latitude').val(pos.lat());
            $('#Longitude').val(pos.lng());

            var results = responses[0].address_components;
            var results1 = responses[1].address_components;

            var cityDetail = findResult(results, "locality");
            var cityFull = findResult(results, "administrative_area_level_1");
            var cityMedium = findResult(results, "administrative_area_level_2");
            var neighborhood = findResult(results1, "neighborhood");
            var city;

            if (cityDetail) {
                city = cityDetail;
            } else if (cityMedium) {
                city = cityMedium;
            } else {
                city = cityFull
            }

            var country = findResult(results, "country");
            $('#City').val(city);
            if (neighborhood) {
                $('#District').val(neighborhood);
            }

            if (infowindow) {
                var address = responses[0].formatted_address;
                infowindow.setContent('<div><strong>' + address + '</strong><br>');
            }
        } else {
            console.log(responses);
        }
    });
}

function initsearchBox() {

    var map = initMap();
    
    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        $('#pac-input').val($('#Address').val());
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {

            if (markers.length != 0) {
                return;
            }

            // Create a marker for each place.
            

            var infoWindow = initInfowindow();
            geocodePosition(marker.getPosition(), infoWindow);
            var text = $("<div/>").html(infoWindow.content).text();
            infoWindow.open(map, marker);

            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition(marker.getPosition(), infoWindow);
                text = $("<div/>").html(infoWindow.content).text();

                $('#Address').val(text);
                infoWindow.open(map, marker);

            });

            markers.push(marker);

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });

        map.fitBounds(bounds);
    });


    $("#Address").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            google.maps.event.trigger(searchBox, 'places_changed');
        }
    });

    $("#Address").on('blur focusout', function () {
        google.maps.event.trigger(searchBox, 'places_changed');
    });

}

var foundLocation = function () {
    //do stuff with your location! any of the first 3 args may be null
    console.log(arguments);
}