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
    })
})
