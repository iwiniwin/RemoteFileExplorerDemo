local obj = CS.UnityEngine.Object.FindObjectOfType(typeof(CS.Game.ChangeTextColor))
local textComponent = obj:GetComponent(typeof(CS.UnityEngine.UI.Text));
textComponent.text = "Hello, World"