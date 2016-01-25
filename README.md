# RedactorPSPlugin

A simple Powershell snapin that provides commands to redact configuration files, making them safe for Github
public repositories. We don't want those black-hats seeing live Amazon keys, database passwords and the like!

Usage:
* ```Add-RedactionToken (raw) (token)``` - adds a token to the dictionary. Typically ```(raw)``` will be a password, an IP address, or similar string that needs to be used in the source to make it function, but must not be published. ```(token)``` is a safe token to be substituted for it.
* ```Clear-RedactionTokens``` - clears the token dictionary. Use with caution as you won't be able to restore files that use any of the cleared tokens.
* ```Show-RedactionTokens``` - shows the current contents of the token dictionary.
* ```Redact-File (path)``` - redacts a single file
* ```Redact-Configs (path)``` - recurses down the given folder, redacting all ```.config``` and ```.settings``` files.
* ```Unredact-File (path)``` - restores a single file to its working state
* ```Unredact-Configs (path)``` - recurses down the given folder, restoring all ```.config``` and ```.settings``` files.
* 
