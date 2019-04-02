(window.webpackJsonp=window.webpackJsonp||[]).push([[0],{31:function(e,t,n){e.exports=n(42)},41:function(e,t,n){},42:function(e,t,n){"use strict";n.r(t);var a=n(0),r=n.n(a),o=n(27),c=n.n(o),l=n(3),i=n(5),u=n(6),s=n(8),E=n(7),m=n(9),p=n(10),d=n(43),S=n(44),h=n(30),f=n(19),b=n(45),O=function(e){var t=e.component,n=Object(f.a)(e,["component"]);return r.a.createElement(h.a,Object.assign({},n,{render:function(e){return localStorage.getItem("user")?r.a.createElement(t,e):r.a.createElement(b.a,{to:{pathname:"/login",state:{from:e.location}}})}}))},g=function(e){var t=e.component,n=Object(f.a)(e,["component"]);return r.a.createElement(h.a,Object.assign({},n,{render:function(e){return localStorage.getItem("user")?r.a.createElement(b.a,{to:{pathname:"/profile",state:{from:e.location}}}):r.a.createElement(t,e)}}))},C=n(17),T=n(24),R=n(12);Object(R.a)();var v={SUCCESS:"ALERT_SUCCESS",ERROR:"ALERT_ERROR",CLEAR:"ALERT_CLEAR"},_={REGISTER_REQUEST:"USERS_REGISTER_REQUEST",REGISTER_SUCCESS:"USERS_REGISTER_SUCCESS",REGISTER_FAILURE:"USERS_REGISTER_FAILURE",LOGIN_REQUEST:"USERS_LOGIN_REQUEST",LOGIN_SUCCESS:"USERS_LOGIN_SUCCESS",LOGIN_FAILURE:"USERS_LOGIN_FAILURE",LOGOUT:"USERS_LOGOUT"},L={DELETE_REQUEST:"BOOK_DELETE_REQUEST",DELETE_SUCCESS:"BOOK_DELETE_SUCCESS",DELETE_FAILURE:"BOOK_DELETE_FAILURE",EDIT_REQUEST:"BOOK_EDIT_REQUEST",EDIT_SUCCESS:"BOOK_EDIT_SUCCESS",EDIT_FAILURE:"BOOK_EDIT_SUCCESS",CREATE_REQUEST:"BOOK_CREATE_REQUEST",CREATE_SUCCESS:"BOOK_CREATE_SUCCESS",CREATE_FAILURE:"BOOK_CREATE_FAILURE",GETALL_REQUEST:"BOOK_GETALL_REQUEST",GETALL_SUCCESS:"BOOK_GETALL_SUCCESS",GETALL_FAILURE:"BOOK_GETALL_FAILURE"},U={success:function(e){return{type:v.SUCCESS,message:e}},error:function(e){return{type:v.ERROR,message:e}},clear:function(){return{type:v.CLEAR}}};var j="https://azurebookapp.azurewebsites.net",y={login:function(e,t){var n={method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({email:e,password:t})};return fetch("".concat(j,"/Account/Login"),n).then(I).then(function(e){return localStorage.setItem("user",JSON.stringify(e)),e})},logout:A,register:function(e){var t={method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify(e)};return fetch("".concat(j,"/Account/Register"),t).then(I)}};function A(){return localStorage.removeItem("user"),fetch("".concat(j,"/Account/Logout")).then(k)}function I(e){return e.text().then(function(t){var n=t&&JSON.parse(t);if(!e.ok){401===e.status&&(A(),window.location.reload(!0));var a=n&&n.message||e.statusText;return n.errors.Password&&alert(n.errors.Password),n.errors.Email&&alert(n.errors.Email),Promise.reject(a)}return n})}function k(e){return e.text().then(function(t){var n=t&&JSON.parse(t);if(!e.ok){401===e.status&&(A(),window.location.reload(!0));var a=n&&n.message||e.statusText;return Promise.reject(a)}return n})}var w="https://azurebookapp.azurewebsites.net",N={create:function(e){var t=JSON.parse(localStorage.getItem("user"));if(t){var n=t.accessToken,a={method:"POST",headers:{"Content-Type":"application/json","Access-Control-Allow-Origin":"*",Authorization:"Bearer "+n},body:JSON.stringify(e)};return fetch("".concat(w,"/Books/Create"),a).then(G)}},getAll:function(){var e=JSON.parse(localStorage.getItem("user"));if(e){var t=e.accessToken,n={method:"GET",headers:{"Content-Type":"application/json","Access-Control-Allow-Origin":"*",Authorization:"Bearer "+t}};return fetch("".concat(w,"/Books/GetAll"),n).then(G)}},update:function(e){var t=JSON.parse(localStorage.getItem("user"));if(t){var n=t.accessToken,a={method:"POST",headers:{"Content-Type":"application/json","Access-Control-Allow-Origin":"*",Authorization:"Bearer "+n},body:JSON.stringify(e)};return fetch("".concat(w,"/Books/Update"),a).then(G)}},delete:function(e){var t=JSON.parse(localStorage.getItem("user"));if(t){var n=t.accessToken,a={method:"POST",headers:{"Content-Type":"application/json","Access-Control-Allow-Origin":"*",Authorization:"Bearer "+n},body:JSON.stringify(e)};return fetch("".concat(w,"/Books/Delete/").concat(e),a).then(G)}}};function G(e){return e.text().then(function(t){var n=t&&JSON.parse(t);if(!e.ok){401===e.status&&window.location.reload(!0);var a=n&&n.message||e.statusText;return n.errors.Title&&alert(n.errors.Title),n.errors.Content&&alert(n.errors.Content),n.errors.Password&&alert(n.errors.Password),Promise.reject(a)}return n})}var D={login:function(e,t){return function(n){var a;n((a={username:e},{type:_.LOGIN_REQUEST,user:a})),y.login(e,t).then(function(e){n(function(e){return{type:_.LOGIN_SUCCESS,user:e}}(e))},function(e){n(function(e){return{type:_.LOGIN_FAILURE,error:e}}(e)),n(U.error(e))})}},logout:function(){return function(e){e({type:_.LOGOUT}),y.logout()}},register:function(e){return function(t){t(function(e){return{type:_.REGISTER_REQUEST,user:e}}(e)),y.register(e).then(function(e){t(function(e){return{type:_.REGISTER_SUCCESS,user:e}}(e)),alert("Registration confirmed successfully")},function(e){t(function(e){return{type:_.REGISTER_FAILURE,error:e}}(e)),t(U.error(e))})}}};var B={create:function(e){return function(t){t({type:L.CREATE_REQUEST}),N.create(e).then(function(e){t(function(e){return{type:L.CREATE_SUCCESS,newBook:e}}(e))},function(e){t(function(e){return{type:L.CREATE_FAILURE,error:e}}(e))})}},getAll:function(){return function(e){e({type:L.GETALL_REQUEST}),N.getAll().then(function(t){e(function(e){return{type:L.GETALL_SUCCESS,books:e}}(t))},function(t){e(function(e){return{type:L.GETALL_FAILURE,error:e}}(t))})}},edit:function(e){return function(t){t({type:L.EDIT_REQUEST,id:e})}},update:function(e){return function(t){N.update(e).then(function(e){t(function(e){return{type:L.EDIT_SUCCESS,updatedBook:e}}(e))})}},delete:function(e){return function(t){t({type:L.DELETE_REQUEST}),N.delete(e).then(function(e){t(function(e){return{type:L.DELETE_SUCCESS,id:e}}(e))},function(e){t(function(e){return{type:L.DELETE_FAILURE,error:e}}(e))})}}};var P=function(e){function t(e){var n;return Object(i.a)(this,t),(n=Object(s.a)(this,Object(E.a)(t).call(this,e))).onSubmit=function(e){e.preventDefault();var t=n.state,a=t.Email,r=t.Password,o=n.props.dispatch;if(a&&r)try{o(D.login(a,r))}catch(c){console.log(c)}},n.state={Email:"",Password:""},n.handleChange=n.handleChange.bind(Object(p.a)(Object(p.a)(n))),n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"handleChange",value:function(e){var t=e.target,n=t.name,a=t.value;this.setState(Object(C.a)({},n,a))}},{key:"render",value:function(){return r.a.createElement("div",{className:"post-container"},r.a.createElement("form",{className:"form",method:"POST",onSubmit:this.onSubmit},r.a.createElement("label",null,"Email")," ",r.a.createElement("br",null),r.a.createElement("input",{placeholder:"Email",type:"email",name:"Email",onChange:this.handleChange})," ",r.a.createElement("br",null),r.a.createElement("label",null,"Password")," ",r.a.createElement("br",null),r.a.createElement("input",{placeholder:"Password",type:"password",name:"Password",onChange:this.handleChange})," ",r.a.createElement("br",null),r.a.createElement("input",{className:"login",type:"submit",value:"Login"})," ",r.a.createElement("br",null),r.a.createElement(T.a,{to:"/register",className:"btn btn-link"},"Register as new user?")))}}]),t}(a.Component);var Q=Object(l.b)(function(e){return{loggingIn:e.authentication}})(P),F=n(16),J=function(e){function t(e){var n;return Object(i.a)(this,t),(n=Object(s.a)(this,Object(E.a)(t).call(this,e))).state={user:{Email:"",Password:""}},n.handleChange=n.handleChange.bind(Object(p.a)(Object(p.a)(n))),n.onSubmit=n.onSubmit.bind(Object(p.a)(Object(p.a)(n))),n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"handleChange",value:function(e){var t=e.target,n=t.name,a=t.value,r=this.state.user;this.setState({user:Object(F.a)({},r,Object(C.a)({},n,a))})}},{key:"onSubmit",value:function(e){e.preventDefault();var t=this.state.user,n=this.props.dispatch;t.Email&&t.Password&&n(D.register(t))}},{key:"render",value:function(){return r.a.createElement("div",{className:"post-container"},r.a.createElement("form",{class:"form",method:"POST",onSubmit:this.onSubmit},r.a.createElement("label",null,"Email")," ",r.a.createElement("br",null),r.a.createElement("input",{type:"email",name:"Email",onChange:this.handleChange})," ",r.a.createElement("br",null),r.a.createElement("label",null,"Password")," ",r.a.createElement("br",null),r.a.createElement("input",{type:"password",name:"Password",onChange:this.handleChange})," ",r.a.createElement("br",null),r.a.createElement("label",null,"Confirm password")," ",r.a.createElement("br",null),r.a.createElement("input",{type:"password",name:"Confirm password"})," ",r.a.createElement("br",null),r.a.createElement("input",{className:"login",type:"submit",value:"Register"})," ",r.a.createElement("br",null),r.a.createElement(T.a,{to:"/login"},"Cancel")))}}]),t}(a.Component);var x=Object(l.b)(function(e){return{registering:e.registration}})(J),K=function(e){function t(){var e,n;Object(i.a)(this,t);for(var a=arguments.length,r=new Array(a),o=0;o<a;o++)r[o]=arguments[o];return(n=Object(s.a)(this,(e=Object(E.a)(t)).call.apply(e,[this].concat(r)))).handleSubmit=function(e){e.preventDefault();var t={title:n.getTitle.value,content:n.getMessage.value,editing:!1};n.props.dispatch(B.create(t)),n.getTitle.value="",n.getMessage.value=""},n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"render",value:function(){var e=this;return r.a.createElement("div",{className:"post-container"},r.a.createElement("h1",{className:"post_heading"},"Create Book"),r.a.createElement("form",{className:"form",onSubmit:this.handleSubmit},r.a.createElement("input",{required:!0,type:"text",ref:function(t){return e.getTitle=t},placeholder:"Enter Post Title"}),r.a.createElement("br",null),r.a.createElement("br",null),r.a.createElement("textarea",{required:!0,rows:"5",ref:function(t){return e.getMessage=t},cols:"28",placeholder:"Enter Post"}),r.a.createElement("br",null),r.a.createElement("br",null),r.a.createElement("button",null,"Post")))}}]),t}(a.Component),z=Object(l.b)()(K),M=function(e){function t(){var e,n;Object(i.a)(this,t);for(var a=arguments.length,r=new Array(a),o=0;o<a;o++)r[o]=arguments[o];return(n=Object(s.a)(this,(e=Object(E.a)(t)).call.apply(e,[this].concat(r)))).handleDeleteClick=function(e){n.props.dispatch(B.delete(e))},n.handleEditClick=function(e){n.props.dispatch(B.edit(e))},n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"render",value:function(){var e=this;return r.a.createElement("div",{className:"post"},r.a.createElement("form",null,r.a.createElement("label",null,this.props.book.dateOfRelease),r.a.createElement("h2",{className:"post_title"},this.props.book.title),r.a.createElement("p",{className:"post_message"},this.props.book.content)," ",r.a.createElement("br",null),r.a.createElement("div",{className:"control-buttons"},r.a.createElement("button",{className:"edit",onClick:function(){return e.handleEditClick(e.props.book.id)}},"Edit"),r.a.createElement("button",{className:"delete",onClick:function(){return e.handleDeleteClick(e.props.book.id)}},"Delete"))))}}]),t}(a.Component),q=Object(l.b)()(M),V=function(e){function t(){var e,n;Object(i.a)(this,t);for(var a=arguments.length,r=new Array(a),o=0;o<a;o++)r[o]=arguments[o];return(n=Object(s.a)(this,(e=Object(E.a)(t)).call.apply(e,[this].concat(r)))).handleEdit=function(e){e.preventDefault();var t={id:n.props.book.id,title:n.getTitle.value,content:n.getMessage.value,authorid:n.props.book.authorId,dateOfRelease:n.props.book.dateOfRelease};n.props.dispatch(B.update(t))},n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"render",value:function(){var e=this;return r.a.createElement("div",{onSubmit:this.handleEdit},r.a.createElement("form",{className:"form"},r.a.createElement("input",{required:!0,type:"text",ref:function(t){return e.getTitle=t},defaultValue:this.props.book.title,placeholder:"Enter Post Title"}),r.a.createElement("br",null),r.a.createElement("br",null),r.a.createElement("textarea",{required:!0,rows:"5",ref:function(t){return e.getMessage=t},defaultValue:this.props.book.content,cols:"28",placeholder:"Enter Post"}),r.a.createElement("br",null),r.a.createElement("br",null),r.a.createElement("button",null,"Update")))}}]),t}(a.Component),W=Object(l.b)()(V),X=function(e){function t(){return Object(i.a)(this,t),Object(s.a)(this,Object(E.a)(t).apply(this,arguments))}return Object(m.a)(t,e),Object(u.a)(t,[{key:"componentWillMount",value:function(){this.props.dispatch(B.getAll())}},{key:"render",value:function(){return r.a.createElement("div",null,r.a.createElement("h1",{className:"post_heading"},"All Books:"),r.a.createElement("br",null),r.a.createElement("br",null),this.props.books.map(function(e,t){return r.a.createElement("div",{key:t},e.editing?r.a.createElement(W,{key:e.id,book:e}):r.a.createElement(q,{key:e.id,book:e}))}))}}]),t}(a.Component),H=Object(l.b)(function(e){return{books:e.books}})(X),Y=function(e){function t(){return Object(i.a)(this,t),Object(s.a)(this,Object(E.a)(t).apply(this,arguments))}return Object(m.a)(t,e),Object(u.a)(t,[{key:"render",value:function(){return r.a.createElement("div",{className:"App"},r.a.createElement("div",{className:"navbar"},r.a.createElement("span",{className:"center"},"Welcome, ",this.props.loggingIn.user.userEmail,"!")),r.a.createElement(z,null),r.a.createElement(H,null))}}]),t}(a.Component);var Z=Object(l.b)(function(e){return{loggingIn:e.authentication}})(Y),$=(n(41),function(e){function t(e){var n;return Object(i.a)(this,t),(n=Object(s.a)(this,Object(E.a)(t).call(this,e))).onLogoutClick=n.onLogoutClick.bind(Object(p.a)(Object(p.a)(n))),n}return Object(m.a)(t,e),Object(u.a)(t,[{key:"onLogoutClick",value:function(e){e.preventDefault(),(0,this.props.dispatch)(D.logout())}},{key:"render",value:function(){var e=this;return r.a.createElement(S.a,null,r.a.createElement("div",null,r.a.createElement("ul",{className:"navbar"},r.a.createElement("li",null,r.a.createElement(d.a,{exact:!0,to:"/"},"Home")),r.a.createElement(function(){return e.props.alert?r.a.createElement("li",null,r.a.createElement(d.a,{to:"/logout",onClick:e.onLogoutClick},"Logout")):r.a.createElement("li",null,r.a.createElement(d.a,{to:"/login"},"Login"))},null)),r.a.createElement("div",null,r.a.createElement(O,{exact:!0,path:"/",component:Z}),r.a.createElement(g,{path:"/login",component:Q}),r.a.createElement(h.a,{path:"/register",component:x}),r.a.createElement(O,{path:"/profile",component:Z}))))}}]),t}(a.Component));var ee=Object(l.b)(function(e){return{alert:e.authentication.authenticated}})($),te=n(13),ne=JSON.parse(localStorage.getItem("user")),ae=ne?{authenticated:!0,user:ne}:{authenticated:!1};null===ne?_.LOGIN_FAILURE:_.LOGIN_SUCCESS;var re=n(15);var oe=Object(te.c)({authentication:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:ae,t=arguments.length>1?arguments[1]:void 0;switch(t.type){case _.LOGIN_REQUEST:return{user:t.user};case _.LOGIN_SUCCESS:return{authenticated:!0,user:t.user};case _.LOGIN_FAILURE:return{error:t.error};case _.LOGOUT:return Object(F.a)({},e,{authenticated:!1});default:return e}},registration:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=arguments.length>1?arguments[1]:void 0;switch(t.type){case _.REGISTER_REQUEST:return{registering:!0};case _.REGISTER_SUCCESS:return{user:t.user};case _.REGISTER_FAILURE:return{error:t.error};default:return e}},alert:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=arguments.length>1?arguments[1]:void 0;switch(t.type){case v.SUCCESS:return{type:"alert-success",message:t.message};case v.ERROR:return{type:"alert-danger",message:t.message};case v.CLEAR:return{};default:return e}},books:function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:[],t=arguments.length>1?arguments[1]:void 0;switch(t.type){case L.CREATE_REQUEST:return Object(re.a)(e);case L.CREATE_SUCCESS:return[].concat(Object(re.a)(e),[t.newBook]);case L.CREATE_FAILURE:case L.DELETE_REQUEST:return Object(re.a)(e);case L.DELETE_SUCCESS:return Object(re.a)(e.filter(function(e){return e.id!==t.id}));case L.DELETE_FAILURE:return Object(re.a)(e);case L.EDIT_REQUEST:return e.map(function(e){return e.id===t.id?Object(F.a)({},e,{editing:!e.editing}):e});case L.EDIT_SUCCESS:return e.map(function(e){return e.id===t.updatedBook.id?Object(F.a)({},e,{title:t.updatedBook.title,content:t.updatedBook.content,editing:!e.editing}):e});case L.EDIT_FAILURE:return[t.error];case L.GETALL_REQUEST:return[];case L.GETALL_SUCCESS:return Object(re.a)(e.concat(t.books));case L.GETALL_FAILURE:return[t.console.error];default:return e}}}),ce=n(29),le=window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__||te.d;var ie,ue=Object(te.e)(oe,ie,le(Object(te.a)(ce.a)));c.a.render(r.a.createElement(l.a,{store:ue},r.a.createElement(ee,null)),document.getElementById("root"))}},[[31,1,2]]]);
//# sourceMappingURL=main.b44e233c.chunk.js.map