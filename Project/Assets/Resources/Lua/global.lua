local m_language

colorYellow = "<color=#FFB820>"

function InitLanguage(language)
	m_language = language
end

function IsTableHasKey(tb, id)
	for k, v in pairs(tb)do
		if v == id then
			return true
		end
	end
	return false
end

function GetText(id)
	if m_language then
		local data = GlobalHooks.dataReader:FindData("localization", id)
		if data then
			return data[m_language]
		end
		return "";
	end
end

