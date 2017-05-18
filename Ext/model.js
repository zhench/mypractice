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

        Ext.regModel("user", {
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

        //实例化person类
        //用new关键字
        var p = new person({
            name: 'uspcat.com',
            age: 26,
            email: 'yunfengcheng2008@ 126. com'
        });

        //alert(p.get('name'));
        var p1 = Ext.create("person", {
            name: 'uspcat.com',
            age: 26,
            email: 'yunfengcheng2008@ 126. com'
        });
        //alert(p1.get('name'));
        var p2 = Ext.ModelMgr.create({
            name: 'uspcat.com',
            age: 26,
            email: 'yunfengcheng2008@ 126. com'
        }, "person");
        //alert(p2.get('email'));
        alert(person.getName());
    });
})();
