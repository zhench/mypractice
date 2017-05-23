/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2017-05-18 16:49:37
 * @version $Id$
 */
require(["dojo/_base/declare"], function(declare) {
    declare("myApp.examples.Shape", null, {
        color: 0,
        setColor: function(color) {
            this.color = color;
        }
    });

    declare("myApp.examples.Circle", myApp.examples.Shape, {
        radius: 0,
        constructor: function(radius) {
            this.radius = radius || this.radius;
        },
        setRadius: function(radius) {
            this.radius = radius;
        },
        area: function() {
            return Math.PI * this.radius * this.radius;
        }
    });

    declare("myApp.examples.Rectangle", myApp.examples.Shape, {
        length: 0,
        width: 0,
        constructor: function(l, w) {
            this.length = l || this.length;
            this.width = w || this.width;
        },
        setLength: function(l) {
            this.length = l;
        },
        setWidth: function(w) {
            this.width = w;
        },
        area: function() {
            return this.length * this.width;
        }
    });
})

require(["dojo/_base/declare"], function(declare) {
    declare("myApp.examples.Position", null, {
        x: 0,
        y: 0,
        constructor: function(x, y) {
            this.x = x || this.x;
            this.y = y || this.y;
        },
        setPosition: function(x, y) {
            this.x = x;
            this.y = y;
        },
        move: function(deltaX, deltaY) {
            this.x += deltaX;
            this.y += deltaY;
        }
    });

    declare("myApp.examples.PositionedCircle", [myApp.examples.Circle, myApp.examples.Position], {
        constructor: function(radius, x, y) {
            this.setRadius(radius);
            this.setPosition(x, y);
        }
    })
})

require(["dojo/_base/declare"], function(declare) {
    declare("myClass", null, {
        globalComplexArg: { val: "foo" },
        localComplexArg: null,
        constructor: function() {
            this.localComplexArg = { val: "bar" };
        }
    })
})
