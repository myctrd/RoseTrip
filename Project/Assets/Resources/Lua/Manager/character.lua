local self = {}
self.__index = self

self.allList = {}
self.myList = {}

function self:HasCharacter(id)
	for k, v in pairs(self.myList) do
		if v == id then
			return true
		end
	end
	return false
end

local function SortList()
	table.sort(self.allList, function(a, b)
        return tonumber(a) < tonumber(b)
    end)
end

function self:GetKingdomList(kingdom)
	local tb = {}
	for k, v in pairs(self.allList) do
		local data = GlobalHooks.dataReader:FindData("character", v)
		if data["KINGDOM"] == kingdom then
			table.insert(tb, v)
		end
	end
	return tb
end

local function DrawACard(outList, weightRange, allList, totalWeight, maxNum)
	if #outList == maxNum then
		return outList
	else
		local value = CS.LuaCallCSUtils.GetRandomValue(totalWeight + 1)
		for k, v in pairs(weightRange) do
			if v >= value and (IsTableHasKey(outList, allList[k]) == false) then
				table.insert(outList, allList[k])
				return DrawACard(outList, weightRange, allList, totalWeight, maxNum)
			end
		end
		return DrawACard(outList, weightRange, allList, totalWeight, maxNum)
	end
end

function self:DrawCards()
	local totalWeight = 0
	local outList = {}
	local weightRange = {}
	for k, v in pairs(self.allList) do
		local data = GlobalHooks.dataReader:FindData("character", v)
		totalWeight = totalWeight + tonumber(data["WEIGHT"])
		table.insert(weightRange, totalWeight)
	end
	outList = DrawACard(outList, weightRange, self.allList, totalWeight, 5)
	GlobalHooks.openUI:OpenUIPanel("UIPanelDrawShow", 2, {list = outList})
end

function self:AddCharacter(id)
	if self:HasCharacter(id) == false then
		table.insert(self.myList, id)
	end
end

function self:LoadCharacter()
	local tb = GlobalHooks.dataReader:GetDataTable("character")
	for k, v in pairs(tb) do
		if tonumber(k) then
			table.insert(self.allList, k)
		end
	end
	SortList()
end

function self:Init()
	self:LoadCharacter()
end

return self