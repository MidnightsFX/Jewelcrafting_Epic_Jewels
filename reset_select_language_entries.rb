# frozen_string_literal: true

require "json"

language_files = Dir["EpicJewels/translations/*"]
keys_to_remove = %w[jc_effect_cover_of_darkness_desc_detail]

language_files.each do |lang_file|
  next if lang_file == "EpicJewels/translations/English.json"

  lang_json = JSON.parse(File.read(lang_file.to_s))
  puts "Removing keys from #{lang_file}"
  keys_to_remove.each do |rm_key|
    lang_json.delete(rm_key)
  end
  File.open(lang_file.to_s, "w") { |f| f.write(JSON.pretty_generate(lang_json)) }
end
