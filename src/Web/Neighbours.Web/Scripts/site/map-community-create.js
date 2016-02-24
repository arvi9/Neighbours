$(document).ready(function () {
    initsearchBox();
});

function initMap() {
    var Sofia = { lat: 42.70115488716061, lng: 23.319979582811357 };

    var map = new google.maps.Map(document.getElementById('map_canvas'), {
        center: Sofia,
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    return map;
}

function initInfowindow() {

    var infowindow = new google.maps.InfoWindow({
        maxWidth: 150
    });

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
    // Create the search box and link it to the UI element.
    var input = document.getElementById('Address');
    var searchBox = new google.maps.places.SearchBox(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

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
            var marker = new google.maps.Marker({
                map: map,
                //icon: icon,
                title: place.name,
                draggable: true,
                position: place.geometry.location
            });

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