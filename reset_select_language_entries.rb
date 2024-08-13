# frozen_string_literal: true

require "json"

language_files = Dir["EpicJewels/translations/*"]
keys_to_remove = %w[jc_effect_add_blunt_damage jc_effect_add_spirit_damage jc_effect_add_pierce_damage jc_effect_add_slash_damage jc_effect_add_lightning_damage jc_effect_add_pickaxe_damage jc_effect_add_chop_damage jc_effect_expert_brawler jc_effect_expert_acrobat jc_effect_expert_daggers jc_effect_expert_smasher jc_effect_expert_spearmaiden jc_effect_expert_sprinter jc_effect_expert_axemaster jc_effect_expert_mage jc_effect_expert_fisher jc_effect_expert_harvester jc_effect_expert_polearms jc_effect_expert_swordsman jc_effect_coin_hoarder jc_effect_coin_greed jc_effect_slash_resistance jc_effect_blunt_resistance jc_effect_pierce_resistance jc_effect_fire_resistance jc_effect_poison_resistance jc_effect_lightning_resistance jc_effect_block_reduce_stamina jc_effect_weapon_reduced_stamina jc_effect_increase_stamina_regen jc_effect_water_swiftness jc_effect_water_frenzy jc_effect_water_resistant jc_effect_inferno jc_effect_burning_frenzy jc_effect_burning_viking jc_effect_reduce_weight jc_effect_cover_of_darkness jc_effect_retribution jc_effect_increase_stamina jc_effect_increase_eitr jc_effect_eitr_conversion]

language_files.each do |lang_file|
  next if lang_file == "EpicJewels/translations/English.json"

  lang_json = JSON.parse(File.read(lang_file.to_s))
  puts "Removing keys from #{lang_file}"
  keys_to_remove.each do |rm_key|
    lang_json.delete(rm_key)
  end
  File.open(lang_file.to_s, "w") { |f| f.write(JSON.pretty_generate(lang_json)) }
end
