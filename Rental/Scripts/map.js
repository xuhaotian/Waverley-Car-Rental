
    mapboxgl.accessToken = 'pk.eyJ1IjoiaHh1dTAwMzUiLCJhIjoiY2sxem1yNmYwMHdpbjNocGNzYTVpNTUzYyJ9.OOjbwsMgKW6LHLj5UWkCNA';
    var map = new mapboxgl.Map({
        container: 'map',
    style: 'mapbox://styles/mapbox/streets-v11',
    center: [144.964, -37.819],
    zoom: 16,
    pitch: 45,
    bearing: -17.6,
    container: 'map',
    antialias: true

    });



map.addControl(new MapboxDirections({
    accessToken: mapboxgl.accessToken
}), 'top-left');


//label
    map.on('load', function () {
        // Add a layer showing the places.
        map.addLayer({
            "id": "places",
            "type": "symbol",
            "source": {
                "type": "geojson",
                "data": {
                    "type": "FeatureCollection",
                    "features": [{
                        "type": "Feature",
                        "properties": {
                            "description": "<strong>Glen Waverley Car Return Point</strong><p>Glen Waverley car return point, Opearning Hours 7:00-18:00 Mon-Sun</p>",
                            "icon": "car"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [145.167, -37.881]
                        }
                    }, {
                        "type": "Feature",
                        "properties": {
                            "description": "<strong>Mad Men Season Five Finale Watch Party</strong><p>Head to Lounge 201 (201 Massachusetts Avenue NE) Sunday for a <a href=\"http://madmens5finale.eventbrite.com/\" target=\"_blank\" title=\"Opens in a new window\">Mad Men Season Five Finale Watch Party</a>, complete with 60s costume contest, Mad Men trivia, and retro food and drink. 8:00-11:00 p.m. $10 general admission, $20 admission and two hour open bar.</p>",
                            "icon": "car"
                        },
                        "geometry": {
                            "type": "Point",
                            "coordinates": [-77.003168, 38.894651]
                        }
                    }]
                }
            },
            "layout": {
                "icon-image": "{icon}-15",
                "icon-allow-overlap": true
            }
        });

    // When a click event occurs on a feature in the places layer, open a popup at the
    // location of the feature, with description HTML from its properties.
        map.on('click', 'places', function (e) {
            var coordinates = e.features[0].geometry.coordinates.slice();
    var description = e.features[0].properties.description;

    // Ensure that if the map is zoomed out such that multiple
    // copies of the feature are visible, the popup appears
    // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
        coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
}

new mapboxgl.Popup()
    .setLngLat(coordinates)
    .setHTML(description)
    .addTo(map);
});

// Change the cursor to a pointer when the mouse is over the places layer.
        map.on('mouseenter', 'places', function () {
        map.getCanvas().style.cursor = 'pointer';
});

// Change it back to a pointer when it leaves.
        map.on('mouseleave', 'places', function () {
        map.getCanvas().style.cursor = '';
});
});



//zomm control
map.addControl(new mapboxgl.NavigationControl());
//3d
    map.on('load', function () {
        // Insert the layer beneath any symbol layer.
        var layers = map.getStyle().layers;

    var labelLayerId;
        for (var i = 0; i < layers.length; i++) {
            if (layers[i].type === 'symbol' && layers[i].layout['text-field']) {
        labelLayerId = layers[i].id;
    break;
}
}

        map.addLayer({
        'id': '3d-buildings',
    'source': 'composite',
    'source-layer': 'building',
    'filter': ['==', 'extrude', 'true'],
    'type': 'fill-extrusion',
    'minzoom': 15,
            'paint': {
        'fill-extrusion-color': '#aaa',

    // use an 'interpolate' expression to add a smooth transition effect to the
    // buildings as the user zooms in
    'fill-extrusion-height': [
        "interpolate", ["linear"], ["zoom"],
        15, 0,
        15.05, ["get", "height"]
    ],
    'fill-extrusion-base': [
        "interpolate", ["linear"], ["zoom"],
        15, 0,
        15.05, ["get", "min_height"]
    ],
    'fill-extrusion-opacity': .6
}
}, labelLayerId);
});
//end 3d


// Add geolocate control to the map.
    map.addControl(new mapboxgl.GeolocateControl({
        positionOptions: {
        enableHighAccuracy: true
},
trackUserLocation: true
}));

    document.getElementById('fly').addEventListener('click', function () {
        // Fly to a random location by offsetting the point -74.50, 40
        // by up to 5 degrees.
        map.flyTo({
            center: [145.167, -37.881],
            zoom: [17],
            pitch: [0],
            bearing: [0]

        });




});

