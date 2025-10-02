 **0.9.23**
 ---
 ```
 - Fixes for Call to Arms
 ``` 

 **0.9.22**
 ---
 ```
 - Damage tooltip supports spellsword
 - Fixes damage trying to find coinhoarder
 ``` 

 **0.9.21**
 ---
 ```
 - Improves damage tooltip preview, adds support for visualizing damage added by coinhoarder
 ``` 

 **0.9.20**
 ---
 ```
 - Fixes for Damage tooltip modification error
 ``` 

 **0.9.19**
 ---
 ```
 - Fixes block effects only taking damage taken into account, not total blocked damage
 ``` 

 **0.9.18**
 ---
 ```
 - Adds scaling and configuration to how strong the farmer autopickup range is
 ``` 

 **0.9.17**
 ---
 ```
 - Improves support for DedicatedServer mod
 ``` 

 **0.9.16**
 ---
 ```
 - Updated Chinese localization
 ``` 

 **0.9.15**
 ---
 ```
 - Fixed skill factor magnitude being too high or too low
 - Fixed skill level not being applied in all cases
 ``` 

 **0.9.14**
 ---
 ```
 - Fixed skill increase being exponential
 ``` 

 **0.9.13**
 ---
 ```
 - Rebalances flat damage gems to be more in line with current jewelcrafting
	- All flat damage gems now have a chance to activate, it is high, the but damage they add is low
 - All skill bonus gems now add skill levels not percentages.
 - Adds a client side configuration to enable/disable tooltip modifications that show damage predictions
 - Adds a limiter to the hover tooltip that will prevent it showing changed stats for non-equipped items
 ``` 

 **0.9.12**
 ---
 ```
 - Fixes Farmer not applying to Thistles
 - Improved disallow list for Farmer
 ``` 

 **0.9.11**
 ---
 ```
 - Fixed a potential error with Cover Of Darkness spawning a tamed bat that is already tamed
 ``` 

 **0.9.10**
 ---
 ```
 - Updated Russian translation
 ``` 

 **0.9.9**
 ---
 ```
 - Fixes a scenario that could cause CombatSpirit to turn all wolves into spirit wolves
 ```

 **0.9.8**
 ---
 ```
 - Fixes potential Error caused by character deaths without a cause
 ```

 **0.9.7**
 ---
 ```
 - Fixes cover of darkness allowing infinite bat spawning with near instant usage weapons
	- Cover of darkness now has a default limit of 2, 4, 6 bats (additive) based on gem level
 - Fixes taming complaints about spawned bats
 ```

 **0.9.6**
 ---
 ```
 - Fix weight reduction displaying a lower inventory weight than it should
 - Fix cover of darkness bats becoming hostile when unloaded and reloaded
 ```

 **0.9.5**
 ---
 ```
 - Bog Witch update, no change.
 ```

 **0.9.4**
 ---
 ```
 - Improvements for translations
 - Fixes missing translations for gem type headers
 ```

 **0.9.3**
 ---
 ```
 - Fix inferno activation chance
 ```

 **0.9.2**
 ---
 ```
 - Changes the damage formula for coinhoarder and sets a soft cap for how large the bonus can get
 ```

 **0.9.1**
 ---
 ```
 - Fix race condition with combat spirit
 ```

 **0.9.0**
 ---
 ```
 - Added Staggering Block
 - Added Burning Guard
 - Added Freezing Guard
 - Added Wet Worker
 - Added Eitr Fused
 - Added Farmer
 - Added Synergy Waterproof
 - Added Synergy Spellsword
 - Fixes Harvest damage types not being applied when harvesting everything
 - Reduces gem default spawn rates in non-primary biomes
 - Added text size auto-resize to support the larger list of synergies
 ```

 **0.7.5**
 ---
 ```
 - Fix localization of gem descriptions
 - Fix localization of gem names
 - Add stylized gem names
 - Fix carry weight gem not reducing item inventory weight contribution
 ```

  **0.7.3**
 ---
 ```
- Adds more gem name localizations
 ```

  **0.7.3**
 ---
 ```
 - Stop localizing some instances of gem names
 ```


 **0.7.2**
 ---
 ```
 - Adds localization to gem names
 - Adds localization to gem socket names
 ```

 **0.7.1**
 ---
 ```
 - Spaces all gem effects
 - fix misspelling of opal
 ```

 **0.7.0**
---
```
- Updated powers with multiple values to no longer be static for the secondary values
- Added new gem powers
	- Retribution, a chance to return some of the blocked damage (can be significant)
	- EitrConversion, a chance to restore Eitr on block
- Added Synergies
	- CombatSpirit - Summon a spirit wolf to aid you when you enter combat, lives for a moderate time and has a respawn cooldown.
	- WeaponMaster - Increase all of the physical weapon skill levels.
	- SlipperyWhenWet - Significant increase to speed when wet
	- IntenseFire - Increases fire resistance and chance to trigger Inferno.
- Added translation support for many languages.
```

 **0.2.6**
---
```
- Updated crystal materials
```

 **0.2.5**
---
```
- Fix Chop damage adjustment being applied correctly
- Add UI feedback for direct bonus damage effects added by this mod
- Changed coingreed to spawn coins in a singular stack
```

 **0.2.4**
---
```
- Fix stamina regen to be a fraction increase
- Adjusting gem colors to reduce crystal whiteness
- Increased base stamina regeneration bonuses from crystals
```

 **0.2.3**
---
```
- Visual display of skill increase percentages from skill gems
```

 **0.2.2**
---
```
- Null check for Coingreed to prevent error on player spawn/respawn
```

 **0.2.1**
---
```
- Fixes stamina consumption for WeaponReducedStamina
```

 **0.2.0**
---
```
- Added CoverOfDarkness to Opal
- Removes Jotunn dependency
- Changes coingreed to apply only to character deaths killed by the player
- Removed excess logging
- Removed debug logging option
```

 **0.1.0**
---
```
- Initial release, I'm sure I forgot something
- Its way past my bedtime
```