# Numerolog - duchovní číslověda

Numerologie německého jazyka duchovně přijatá Lucienem Siffridem a Hermannem Wenngem z kruhu Pána Abdrushina.
- Původní [abeceda](Numerology/Alphabets.cs#:~:text=static%20readonly%20Alphabet-,German,-%3D%20new()) byla rozšířena o písmeno ß s číselnou hodnotou dvou s = 1 + 1 = 2.

Aplikace je dostupná na adrese https://numerolog.vzestup.net a lze ji z [prohlížeče nainstalovat](https://support.google.com/chrome/answer/9658361?hl=cs&co=GENIE.Platform%3DDesktop) a používat offline.

## Souvislosti

Aplikace byla použita na výpočet [výstavby Poselství Grálu](https://abdrushin.one/cs/poselstvi_gralu/1931/vystavba#numerologie).

Existuje podobná a propracovanější aplikace od Juraje Sitára [Nová Numerológia Slovenov - Nová Slovenská číslovéda](https://yaspis.sk/programy-jaspis#:~:text=Nov%C3%A9%20Hodiny%20Slovenov%C2%A0%20%C2%BB-,Nov%C3%A1%20Numerol%C3%B3gia%20Slovenov,-%2D%20Nov%C3%A1%20Slovensk%C3%A1%20%C4%8D%C3%ADslov%C3%A9da).
- Nepodporuje německé znaky Ä, Ü, Ö, ß.

## Části aplikace

| Adresa | Význam |
| ---    | ------ |
| [/](https://numerolog.vzestup.net/) | Výpočet numerologie textu |
| [/spectra](https://numerolog.vzestup.net/spectra) | Test barevných spekter |
| [/colorMixers](https://numerolog.vzestup.net/colorMixers) | Test slučování barev |

## Historie

- 2023.5.14
  - základní testování barevných spekter a slučování barev
  - zadávání názvu je při větších výpočtech plynulejší
  - výsledné trojčíslí a číslo v titulku stránky
  - zřetelnější ikona na různých pozadích
- 2023.5.8
  - adresa stránky obsahuje její obsah pro možnost sdílení
  - optimalizace výpočtu na pozadí
  - signalizace průběhu výpočtu
  - tisknutí jen textů s výpočty bez záhlaví aplikace
  - možnost zadat název výpočtu zobrazený v titulku stránky a při tisku
- 2023.5.7
  - zveřejnění první verze

## Technologie

Aplikace je napsaná v .NET 7 a skládá se z těchto částí:
- Numerology
  - C# knihovna pro numerologické výpočty
- Numerolog
  - ASP.NET Core Blazor WebAssembly aplikace

## Licence

Použití aplikace a zdrojového kódu v souladu s Božími zákony.

## Autor

[Marek Ištvánek](https://duchovnipodpora.vzestup.net/user/rolfik)