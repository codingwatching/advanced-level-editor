"use strict";(self.webpackChunkale_docs=self.webpackChunkale_docs||[]).push([[480],{3905:function(e,t,n){n.d(t,{Zo:function(){return c},kt:function(){return m}});var r=n(7294);function i(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function o(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){i(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function p(e,t){if(null==e)return{};var n,r,i=function(e,t){if(null==e)return{};var n,r,i={},a=Object.keys(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||(i[n]=e[n]);return i}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(i[n]=e[n])}return i}var l=r.createContext({}),s=function(e){var t=r.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):o(o({},t),e)),n},c=function(e){var t=s(e.components);return r.createElement(l.Provider,{value:t},e.children)},d={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},u=r.forwardRef((function(e,t){var n=e.components,i=e.mdxType,a=e.originalType,l=e.parentName,c=p(e,["components","mdxType","originalType","parentName"]),u=s(n),m=i,f=u["".concat(l,".").concat(m)]||u[m]||d[m]||a;return n?r.createElement(f,o(o({ref:t},c),{},{components:n})):r.createElement(f,o({ref:t},c))}));function m(e,t){var n=arguments,i=t&&t.mdxType;if("string"==typeof e||i){var a=n.length,o=new Array(a);o[0]=u;var p={};for(var l in t)hasOwnProperty.call(t,l)&&(p[l]=t[l]);p.originalType=e,p.mdxType="string"==typeof e?e:i,o[1]=p;for(var s=2;s<a;s++)o[s]=n[s];return r.createElement.apply(null,o)}return r.createElement.apply(null,n)}u.displayName="MDXCreateElement"},342:function(e,t,n){n.r(t),n.d(t,{frontMatter:function(){return p},contentTitle:function(){return l},metadata:function(){return s},toc:function(){return c},default:function(){return u}});var r=n(7462),i=n(3366),a=(n(7294),n(3905)),o=["components"],p={sidebar_position:3},l="Unity Primitives",s={unversionedId:"serialization/unity-primitives",id:"serialization/unity-primitives",title:"Unity Primitives",description:"The most common Unity primitives can be used and serialized by the serializer. These types include",source:"@site/docs/serialization/unity-primitives.md",sourceDirName:"serialization",slug:"/serialization/unity-primitives",permalink:"/advanced-level-editor/docs/serialization/unity-primitives",editUrl:"https://github.com/hertzole/advanced-level-editor/tree/master/docs/docs/serialization/unity-primitives.md",tags:[],version:"current",sidebarPosition:3,frontMatter:{sidebar_position:3},sidebar:"tutorialSidebar",previous:{title:"C# Primitives",permalink:"/advanced-level-editor/docs/serialization/c-primitives"},next:{title:"Custom Types",permalink:"/advanced-level-editor/docs/serialization/custom-types"}},c=[],d={toc:c};function u(e){var t=e.components,n=(0,i.Z)(e,o);return(0,a.kt)("wrapper",(0,r.Z)({},d,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h1",{id:"unity-primitives"},"Unity Primitives"),(0,a.kt)("p",null,"The most common Unity primitives can be used and serialized by the serializer. These types include\n",(0,a.kt)("inlineCode",{parentName:"p"},"AnimationCurve"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Bounds"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"BoundsInt"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Color32"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Color"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"GradientAlphaKey"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"GradientColorKey"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Gradient"),",\n",(0,a.kt)("inlineCode",{parentName:"p"},"Keyframe"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"LayerMask"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Matrix4x4"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Quaternion"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"RangeInt"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Rect"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"RectInt"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"RectOffset"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Vector2"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Vector2Int"),",\n",(0,a.kt)("inlineCode",{parentName:"p"},"Vector3"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Vector3Int"),", ",(0,a.kt)("inlineCode",{parentName:"p"},"Vector4"),", and ",(0,a.kt)("inlineCode",{parentName:"p"},"WrapMode")),(0,a.kt)("p",null,"It also supports all these types as an array (for example ",(0,a.kt)("inlineCode",{parentName:"p"},"Color[]"),"), as lists (for example ",(0,a.kt)("inlineCode",{parentName:"p"},"List<Color>"),"), and as nullable\n(for example ",(0,a.kt)("inlineCode",{parentName:"p"},"Color?"),")."))}u.isMDXComponent=!0}}]);