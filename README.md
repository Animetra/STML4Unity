# Animetra ScreenTexter
A Unity module to load and display screen text

## Concept

Animetra ScreenTexter understands a screentext as a series of statements, done by certain speakers. The most adequate use case would be a Zelda-like dialogue.
It's not meant to implement subtitles (but it can be used for that), but rather replacing a voice with text.

The most important features are:

- Importing texts in an XML-like format
- Rich Text Format functionality, [have a look here](https://docs.unity3d.com/Packages/com.unity.ugui@3.0/manual/StyledText.html)
- build up effect
- Light-weight structuring tools

## Workflow

1. Write your screentext into a .stml-file (= ScreenText Markup Language).
1. Create a `new STMLParser()` and use `STMLParser.LoadFile(filePath)` to load your .stml-file and get back a `STMLScript`.
1. Use `STMLScript.StartStatement(0)` to start with the first statement.
1. Use `STMLScript.GetFormattedStatement(float deltaTime)` to get the current partial string of that statement.
1. Display your string in any way you want.

## STML

STML is a XML format to easily write screentext, understandable for the ScreenTexter.
Each STML-Document follows this structure (replace the contents of the `[ ]`):

	<root>
		<header>
			<title>[Your tite]</title>
			<description>[What is this document about?]</description>
			<language>[language code of this document's language]</language>
			<language_original>[language code of the original document's language]</language_original>
			<author>[Who has written this document?]</author>
			<author_original>[Who has written the original document?]</author_original>
			<version>[Version of this document]</version>
		</header>

		<screentext>
			<section id="[Unique name for this conversation]">
				<statement speaker="Speaker A">
					[content]
					[you can use RTF here]
				<statement>
		
				<statement speaker="Speaker B" style="Shout">
				...
				</statement>

				...
			</section>
		</screentext>
	</root>

The language code has to follow [ISO-639-1](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) in lower case:
English -> en
German -> de
...and so on


## Restrictions

- Nested RTF tags is not possible, you have to use styles from a style guide for now:
