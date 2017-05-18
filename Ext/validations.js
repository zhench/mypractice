(function() {
    Ext.onReady(function() {
        //里用Ext.define创建模型类
        //DB table person(name,age,email)
        Ext.define("person", {
            extend: "Ext.data.Model",
            fields: [{
                name: 'name',
                type: 'auto'
            }, {
                name: 'age',
                type: 'int'
            }, {
                name: 'email',
                type: 'auto'
            }]
        });
        var p1 = Ext.create("person", {
            name: 'uspcat.com',
            age: 26,
            email: 'yunfengcheng2008@ 126. com'
        });
        /*
         * name 2~6 在set()
         */
    });
})();
