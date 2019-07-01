local self = {}
self.__index = self

self.menu = require 'UI.UIPanels.UIPanelMenu'
self.settings = require 'UI.UIPanels.UIPanelSettings'
self.gameDeck = require 'UI.UIPanels.UIPanelGameDeck'

self.floatingMsg = require 'UI.UIComponents.UIComponentFloatingMsg'

function self:InitUIComponent(name, obj, sort, params)
	if name == nil then
		return
	end
	if name == "UIComponentFloatingMsg" then
		self.floatingMsg:InitUI(name, obj, sort, params)
	end
end

function self:OpenUIPanel(name, sort, params)
	if name == nil then
		return
	end
	if sort == nil then
		sort = 1
	end
	if name == "UIPanelMenu" then
		self.menu:InitUI(name, sort, params)
	elseif name == "UIPanelSettings" then
		self.settings:InitUI(name, sort, params)
	elseif name == "UIPanelGameDeck" then
		self.gameDeck:InitUI(name, sort, params)
	end
end

return self