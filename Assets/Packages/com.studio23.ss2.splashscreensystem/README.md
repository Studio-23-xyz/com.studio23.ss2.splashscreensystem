<h1 align="center">Splash Screen System</h1><p align="center">
<a href="https://openupm.com/packages/com.studio23.ss2.splashscreensystem/"><img src="https://img.shields.io/npm/v/com.studio23.ss2.splashscreensystem?label=openupm&amp;registry_uri=https://package.openupm.com" /></a>
</p>

Splash Screen System is used for Show Splash Screen before starting game. Generally Its shown Disclaimer, EULA & 3rd Party pakages.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)

## Installation

### Install via Git URL

You can also use the "Install from Git URL" option from Unity Package Manager to install the package.

```
https://github.com/Studio-23-xyz/com.studio23.ss2.splashscreensystem.git#upm
```

## Usage

### Getting Started

To start using the Splash Screen System. You need to take a few setup stepts

1. Click on the 'Studio-23' available on the top navigation bar and navigate to Splash Screen System > Widget to create Disclaimer's Title & Description, EULA's Title & Description & 3rd Party's Images Data Scriptableobject.

2. Then You need to assign those scriptable object into Inspector under Splash Screen Behaviour script.

3. From here you can set Duration and Fadeduration.

### Using The Splash Screen System

1. First Create a canvas, Then Create a Empty Game Object.

2. Under Game object need to create a panel.

3. Now, Need to create Title (Text), For Showing Title.

4. Need To Create Scroll View. Under Viewport need to create a Description (Text) for showing Description.

5. Then Need to create a Empty Game Object, Which contain two Button (AcceptButton, Decline Button)

6. At last, you need to create GameObject which may contain 3rd Party's Logos.

Scroll View, Button Panel (Game Object) [Under Button Panel ]

````Csharp
Use the '```' symbols to start and end a code block. The starting symbol is followed by the language type used.
````
