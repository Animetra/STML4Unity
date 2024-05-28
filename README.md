# Animetra ScreenTexter
A C# module to load and return screen text.
Can be used for translation and implementing on-screen text conversation.

## Concept

Animetra ScreenTexter reads text saved in XML-inspired documents and returns it either as unformatted string or with RTF style tags to be used in Unity.

The most important features are:

- Importing texts in an XML-like format (STML = ScreenText Markup Language)
- Rich Text Format functionality, [have a look here](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/StyledText.html)
- Light-weight structuring tools
- many use cases

## Quick Start

Do this to get the formatted text of an expression:

1. Write your text into a .stml-file.
1. Create a `new STMLReader()` and use `STMLReader.ReadFile()` to load your .stml-file and get back a `STMLDocument`.
1. Use `STMLDocument.GetSection()` to get the section with your desired expression via its id.
1. Use `STMLSection.GetExpression()` to get the expression you want via its id or its index.
1. Use `STMLExpression.GetFormattedText` to get the expression's formatted text or `STMLExpression.GetPlainText` to get the unformatted text.

## STML

STML is a XML format to easily write screentext, understandable for the ScreenTexter.
Each STML-Document follows this structure (replace the contents of the `{ }`):


	<?xml version="1.0" encoding="utf-8"?>
	<root>
		<stml version="{ }"/>
	
		<header>
			<title>Document</title>
			<description>This is an example stml document to demonstrate, how ScreenTexter works.</description>
			<language>en</language>
			<author>A clever Animetra employee</author>
			<version>1.0.0</version>
		</header>

		<screentext>
			<section id="testSection">
			
				<term id="testTerm">
					This is a term.
				</term>
			
				<!-- add more here -->
				
				<expression>
					<style class="H1">This is a Header.</style> And this an expression text. You can use RTF here, e.g. you can make this <b>bold</b>, <i>italic</i> or <b><i>both nested</i></b>. <color value="red">Colors</color> work too, as well as <size value="30">different</size> <size value="50">sizes.</size>
				</expression>
			
				<expression id="testExpression" style ="Highlighted">
					Use the optional style-attribute to style the whole expression with a style from your style sheet. With the id attribute you can identify this expression from the corresponding STMLExpression-object via the overload "STMLExpression GetExpression(string id)".
				</expression>
			
				<expression narrator="A">
					Use the attribute "narrator", if your text is spoken by a character to give this document a more script-like vibe and you know who says what. If you use `STMLExpression.GetFormattedText(true)`, you can define an own style in your style sheet named after your narrator (in this case "A") and each expression of this narrator will be formatted in that style.
				</expression>
		
				<expression narrator="B" style="Shout">
					You can also use the optional style attribute to give narrator expressions a special style. It's nested inside the narrator style, so use this for special occasions like shouting, whispering and so on.
				</expression>
			
				<!-- add more here -->
			</section>
	
			<!-- add more here -->
		</screentext>
	</root>



The language code has to follow [ISO-639-1](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) in lower case:
English -> en
German -> de
...and so on


## Restrictions

- Nested RTF tags is not possible, you have to use styles from a style guide for now:
