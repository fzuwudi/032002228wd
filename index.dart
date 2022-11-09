main() {
  // print('hello~dart!');
  // var str = "hello,world!";
  // print(str);
  // var num = 123456;
  // print(num);
  // final a = new DateTime.now();
  // print(a);

  // String str = '''hhh
  // aaa
  // bbb
  // ''';
  // print(str);

  // String str1 = 'hello';
  // String str2 = 'dart';
  // print("$str1 $str2");
  // print(str1 + ' ' + str2);

  // var l1 = ['haha', 2, true];
  // print(l1);
  // print(l1.length);
  // print(l1[0]);
  // print(l1[1]);

  // var l3 = [];
  // l3.add("hhh");
  // l3.add("aaa");
  // l3.add(2);
  // print(l3.length);

  // var person = {
  //   "name": "zhang san",
  //   "age": 20,
  //   "work": ["程序员", "外卖骑手"]
  // };
  // print(person);
  // print(person["name"]);
  // print(person["age"]);
  // print(person["work"]);

  // var p = new Map();
  // p["name"] = "haha";
  // p["age"] = 23;
  // p["work"] = ["搬砖"];
  // print(p);
  // print(p["work"]);

  // String str = '1';
  // if (str is String) {
  //   print('is String');
  // } else {
  //   print('is not String');
  // }
  // var myNum = int.parse(str);
  // print(myNum);
  // String str1 = '123.1';
  // var myNum1 = double.parse(str1);
  // print(myNum1);
  // var number = 234;
  // var str3 = number.toString();
  // print(str3);

  // var myNum;
  // if (myNum == null) {
  //   print('null');
  // } else {
  //   print('not null');
  // }

  // List myList = ['banana', 'apple', 'orange'];
  // myList.add('peach');
  // myList.addAll(['grape', 'watermelon']);
  // var list1 = myList.reversed.toList();
  // print(list1);
  // print(myList.indexOf('orange'));
  // myList.remove('watermelon');
  // print(myList);
  // myList.removeAt(1);
  // print(myList);
  // myList.fillRange(1, 2, 'strawberry');
  // print(myList);
  // myList.insert(1, 'pear');
  // myList.insertAll(1, ['mango', 'blueberry']);
  // print(myList);
  // var str = myList.join('+');
  // print('我想吃 ' + str);
  // var list2 = str.split('+');
  // print(list2);

  // List myList = ['banana', 'apple', 'orange', 'apple', 'banana'];
  // var s = new Set();
  // s.addAll(myList);
  // myList = s.toList();
  // print(myList);

  // var person = {
  //   "name": "zhang san",
  //   "age": 20,
  //   "work": ["程序员", "外卖骑手"]
  // };
  // print(person.keys.toList());
  // print(person.values.toList());
  // print(person.isEmpty);
  // person.addAll({"height": 185, "weight": 80});
  // print(person);
  // print(person.containsValue("zhang san"));

  // var myList = ['香蕉', '苹果', '梨子'];
  // // for (var item in myList) {
  // //   print(item);
  // // }
  // myList.forEach((value) {
  //   print(value);
  // });

  // List myList = [1, 3, 4];
  // var newList = myList.map((value) {
  //   return value * 2;
  // }).toList();
  // print(newList);

  // List myList = [1, 3, 4, 5, 7, 8, 9];
  // var newList = myList.where((value) {
  //   return value > 5;
  // }).toList();
  // print(newList);
  // var f = myList.any((value) {
  //   return value > 5;
  // });
  // print(f);
  // var f1 = myList.every((value) {
  //   return value > 5;
  // });
  // print(f1);

  // var s = new Set();
  // s.addAll([1, 2, 333]);
  // s.forEach((value) {
  //   print(value);
  // });

  // var person = {
  //   "name": "zhang san",
  //   "age": 20,
  //   "work": ["程序员", "外卖骑手"]
  // };
  // person.forEach((key, value) {
  //   print("$key~~~$value");
  // });

  // fn1() {
  //   print('fn1');
  // }

  // fn2(fn) {
  //   fn();
  // }
  // // 相当于：fn=(){
  // //   print('fn1');
  // // }
  // //匿名函数
  // fn2(fn1);

  // var list = [1, 2, 3];
  // list.forEach((value) => print(value)); //箭头函数

  // var newList = list.map((value) {
  //   if (value > 2) {
  //     return value * 2;
  //   }
  //   return value;
  // });
  // print(newList);

  fn() {
    var a = 123;
    return () {
      a++;
      print(a);
    };
  }

  var b = fn();
  b();
  b();
  b(); //闭包：存在于全局，却不污染环境
}
