/**
 * _BaseWidget.js
 * @authors Your Name (you@example.org)
 * @date    2017-06-09 19:29:41
 * @version $Id$
 */

define(["dojo/_base/declare", "dojo/_base/array", "dojo/query", "dojo/dom-attr", "dojo/dom-style", "dojo/on", "dojo/_base/lang", "./_Widget", "dojo/text!./template/_BaseWidget.html"], function(declare, array, query, domAttr, domStyle, on, lang, _Widget, template) {
    return declare(_Widget, {
        constructor: function( /*Object*/ param) {
            this.connnects = [];
            this.widgets = {};
        },
        templateString: template,
        panels: null,
        panelIndex: -1,

        postMixInProperties: function() {
            if (this.icon == "") {
                this.icon = "assets/image/icon/i_pushpin.png";
            }
        },
        postCreate: function() {
            //如果存在多个面板，则只显示第一个
            this.panels = query(".widgetPanel", this.domNode);
            this.panels = forEach(function(item, idx, arr) {
                item.buttonIcon = domAttr.get(item, "buttonIcon");
                item.buttonText = domAttr.get(item, "buttonText");
            });
            this.showPanel(0);
        }

    })
})
