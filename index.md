---
layout: default
title: Tort and Joie
tagline: Not cool, not cool
description: A blog by Tort and Joie
---

[Github repository](https://github.com/jaypalexa/tortandjoie) for this site.

Things I like to do are:

- Drive down roads
- Think about pancakes

Some pages:

- [Overview](pages/overview.html)
- [Making an independent website](pages/independent_site.html)
- [Making a personal site](pages/user_site.html)
- [Making a site for a project](pages/project_site.html)
- [Making a jekyll-free site](pages/nojekyll.html)
- [Testing your site locally](pages/local_test.html)
- [Resources](pages/resources.html)

---

See more turtle goodness [on TurtleGeek.com](http://www.turtlegeek.com).

---

<ul>
  {% for post in site.posts %}
    <li>
      <a href="{{ post.url }}">{{ post.title }}</a>
      {{ post.excerpt }}
    </li>
  {% endfor %}
</ul>

---

![Paradise Bay]({{ site.url }}/assets/images/tort-and-joie-paradise-bay.jpg)
