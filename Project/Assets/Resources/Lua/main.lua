
GlobalHooks = {
	dataReader = require 'Logic.dataReader',
	openUI = require 'Logic.OpenUI',
	uiUitls = require 'Logic.UIUtils',
	character = require 'Manager.character',
	eventManager = require 'Logic.EventManager',
}

function OpenUIPanel(panel)
	GlobalHooks.openUI:OpenUIPanel(panel)
end

function GetData(tb, id)
	return GlobalHooks.dataReader:FindData(tb, id)
end

function InitUI()
	GlobalHooks.uiUitls:Init()
	--GlobalHooks.character:Init()
	GlobalHooks.openUI:OpenUIPanel("UIPanelMenu")
end
