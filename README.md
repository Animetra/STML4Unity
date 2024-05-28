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

## Structure

| Term | represented by C# class | does |
| --- | --- | --- |
| Reader | `STMLReader` | Reads a .stml-file and returns a `STMLDocument` |
| Library | `STMLLibrary` | A collection of `STMLDocuments` to make working with them easier |
| Element | `STMLElement` | A base class for `STMLDocument`, `STMLSection`, `STMLTerm` and `STMLExpression`.|
| Document | `STMLDocument` | A comfortable data structure based on a .stml-file. Gives easy access to the screentext. |
| Header | `STMLHeader` | The header of a `STMLDocument` |
| Section | `STMLSection` | A structural unit inside a `STMLDocument` to organize your texts. Can contain terms and expressions. |
| Term | `STMLTerm` | A single word or term to be referenced in other texts. Can't be formatted. |
| Expression | `STMLExpression` | A formattable phrase, statement or short text |
| Variable | - | To be used as shorthands in expressions |

## Quick Start

Do this to get the formatted text of an expression:

1. Write your text into a .stml-file.
1. Create a `new STMLReader()` and use `STMLReader.ReadFile()` to load your .stml-file and get a `STMLDocument`.
1. Use `STMLDocument.GetSection()` to get the section with your desired expression via its id.
1. Use `STMLSection.GetExpression()` to get the expression you want via its id or its index.
1. Use `STMLExpression.GetFormattedText` to get the expression's formatted text or `STMLExpression.GetPlainText` to get the unformatted text.

## STML

STML is a XML format to easily write screentext, understandable for the ScreenTexter.
Each STML-Document follows this structure (replace the contents of the `{ }`):

> :construction: Example missing 



The language code has to follow [ISO-639-1](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) in lower case:
English -> en
German -> de
...and so on

## Style options inside expressions

| tag | does | attributes | mandatory / optional | defines |
| --- | --- | --- | --- | --- |
| \<b> | makes text <b>bold</b> | | | |
| \<i> | makes text <i>italic</i> | | | | 
| \<size> | changes <span style="font-size:30px;">text size</span> | value | mandatory | size in pt | 
| \<color> | changes <span style="color:red">text color</span> | value | mandatory | the color in HEX or as keyword | 
| \<material>  |:construction: | value | mandatory | :construction: |
| \<quad> |:construction: | value | mandatory | :construction: |
| \<style> | refers to a style defined in a style sheet | class | mandatory | the class name |
| \<ref>| references and inserts a term (same or other document) | document | mandatory | the id* of the STML document holding the term |
|		|					| section | mandatory | the id* of the section holding the term |
|		|					| term | mandatory | the id of the term |
| \<resource>| inserts a resource (same document) | var | mandatory | the id of the variable |

*You can use "this" to reference the own document or section.

## How to use variables

Variables can be used to hold content and insert it in expressions by referencing the respective variable.
This is especially useful for references to terms you use a lot. Just reference it once, save it into a variable and use that variable.

1. If not already existing, create a `<resources> </resources>`-element into the root element.
1. Into that insert a `<variable></variable>`-element and give it an unique id via the `id`-attribute.
1. Write your desired content into the `variable`-element. You can do everything here you can also do in `expression`s, like RTF-formatting or `<ref>`s
1. To insert the just saved content into an expression, use `<var resource={id}/>` (replace "id" with your chosen variable id).

