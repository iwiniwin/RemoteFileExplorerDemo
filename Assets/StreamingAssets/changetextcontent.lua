local obj = CS.UnityEngine.Object.FindObjectOfType(typeof(CS.Game.ChangeTextColor))
local textComponent = obj:GetComponent(typeof(CS.UnityEngine.UI.Text));
textComponent.text = "Hello, Wlrod"

-- xlua.hotfix(CS.Game.ChangeTextColor, "Start", function(self)
--     self:GetComponent(typeof(CS.UnityEngine.UI.Text)).color = CS.UnityEngine.Color.green
-- end)