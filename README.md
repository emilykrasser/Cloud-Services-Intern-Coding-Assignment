# Cloud Services Intern Coding Assignment
## Emily Krasser
## 2019
This is the repository for my solution to the Unity Cloud Services Intern Coding Assignment for the summer of 2019.

## Overview
For this project, I used ASP.NET C# and used a MVC setup.
In order to tackle this challenge, I broke down what I needed to do into smaller parts and then came up with methods to go about solving those smaller parts.
1. The first order of operations was to ficure out how to get the information from both the Made with Unity page and the various project pages after that. Being as there was no API to call in the source, I retrieved the content via web scraping. The best tool I found to do this was [Html Agility Pack] (https://html-agility-pack.net). This allowed me to easily access and store selected Html nodes in a loaded webpage
	- First I retrieved the Html nodes that corresponded to the various projects and their links on the [Made with Unity] (https://unity.com/madewith) page and stored them in a list in the HomeController.
		-Note: I cached these URLs server-side due to the information retrieval being quicker than cookies. This cache expires after any information has not been retrieved from it within 12 hours. The drawback with caching 
	- Secondly, I created a Showcase class in the Models folder. It holds five lists to store specific Html nodes in. I was able to glean most of the information off of the project page by searching for nodes that included the tag "col-xs-12". The tough part about this was that it included images with src attributes that held relative paths. Finding these nodes and setting their absolute paths solved the problem. The nodes that were left, were the ones that held the galleries, videos, and backgorund images. These were found via "section-article-gallery", "component-gallery-item", "background-image", "section-trailer embed-responsive".
		- For the background images I had to get the whole image tag as a string and then parse of the URL. In the HTML of the Index page I loaded those absolute paths into an image tag to be displayed at the bottom of the page. The first of the images was also used as the background of each showcase's page.
		- The videos were the last part of the project that I needed to finish and they were the most confusing part since I didn't immediately realize that the video url data was stored in one of the div elements as attributes and had to look at the network calls that were made from those elements. Once I realized that, it was easy enough to set the full link and then embed the video for YouTube and Vimeo.
2. The second part of the project was selecting a random project to show until all the projects had been shown and then repeating this process. I did this by creating a list of numbers that were the indices of the projects that the user had not already seen. I cached these servreside


## How the Design Meets the Requirements

## Improvements for the Future
