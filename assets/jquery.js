$("h1, h2, h3").addClass("border-bottom pb-1");

$(".container > :header").addClass("header-margin");

$("code").not("pre > code").addClass("hljs");

$(".alert").find("a").addClass("alert-link");

$(":header.header-margin").not("h1").addClass("anchored");

$(":header").addClass("header");
