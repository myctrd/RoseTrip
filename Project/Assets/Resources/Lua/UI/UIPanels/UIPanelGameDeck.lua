local self = {}
self.__index = self

local function UpdateGold()
	if self.gold ~= nil then
		local value = CS.LuaCallCSUtils.GetPlayerGold() - self.gold
		if value > 0 then
			GlobalHooks.uiUitls:ShowFloatingMsg(GetText("Gold").." +"..value)
		end
	end
	self.gold = CS.LuaCallCSUtils.GetPlayerGold()
	self["txt_gold"]:SetText(self.gold)
end

local function OnExit()
	GlobalHooks.eventManager:RemoveListener("Common.UpdateGold", UpdateGold)
end

local function OnEnter()
    GlobalHooks.eventManager:AddListener("Common.UpdateGold", UpdateGold)
end

local function OnClickQuit()
	OnExit()
	self.ui:Close()
	GlobalHooks.openUI:OpenUIPanel("UIPanelMenu")
	CS.LuaCallCSUtils.UnloadGameScene()
end

local function OnClickStart()
	CS.LuaCallCSUtils.RoleStartMove()
	self["btn_start"]:SetActive(false)
end


local UIName = {
	"btn_quit",
	"btn_start",
	"txt_gold",
}

local function FindUI()
	for i = 1, #UIName do
		self[UIName[i]] = self.ui:GetChild(UIName[i])
	end
	self["btn_quit"]:AddListener(OnClickQuit)
	self["btn_start"]:AddListener(OnClickStart)
	UpdateGold()
end

function self:InitUI(name, sort, params)
	self.params = params
	self.ui = CS.UIManager.m_instance:ShowOrCreatePanel(name, sort)
	OnEnter()
	FindUI()
end

return self