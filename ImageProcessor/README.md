Projekt 1 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

Rysowanie trzech prymitywów: linii, prostokątu, okręgu,
Podawanie parametrów rysowania za pomocą pola tekstowego (wpisanie parametrów w pola tekstowe i zatwierdzenie przyciskiem),
Rysowanie przy użyciu myszy (definiowanie punktów charakterystycznych kliknięciami),
Przesuwanie przy użyciu myszy (uchwycenie np. za krawędź i przeciągnięcie),
Zmiana kształtu / rozmiaru przy użyciu myszy (uchwycenie za punkty charakterystyczne i przeciągnięcie),
Zmiana kształtu / rozmiaru przy użyciu pola tekstowego (zaznaczenie obiektu i modyfikacja jego parametrów przy użyciu pola tekstowego).

Projekt 2 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

Wczytywanie i wyświetlanie plików graficznych w formacie PPM P3,
Wczytywanie i wyświetlanie plików graficznych w formacie PPM P6,
Obsługa błędów (komunikaty w przypadku nieobsługiwanego formatu pliku oraz błędów w obsługiwanych formatach plików),
Wydajny sposób wczytywania plików (blokowy zamiast bajt po bajcie),
Wczytywanie plików JPEG,
Zapisywanie wczytanego pliku w formacie JPEG,
Możliwość wyboru stopnia kompresji przy zapisie do JPEG,
Skalowanie liniowe kolorów,
Proszę nie używać gotowych bibliotek do wczytywania plików PPM.
Program będzie testowany na dostarczonych przez prowadzącego obrazkach.

Projekt 3 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:
a. Konwersja przestrzeni barw:

Dwie możliwości wyboru koloru przez użytkownika: RGB orac CMYK (narzędzia do wyboru koloru mogą być wzorowane na popularnych programach graficznych),
Wybór koloru powinny odbywać się zarówno za pomocą myszy, jak i poprzez wprowadzenie poszczególnych wartości w pola tekstowe,
Wybrany kolor powinien zostać zaprezentowany oraz przekonwertowany na drugi format, tzn. na CMYK przy wyborze RGB oraz na RGB przy wyborze CMYK przy użyciu wzorów podanych w treści zadania,
Wartości wybrane przez użytkownika oraz po konwersji powinny zostać wyświetlone.
Spośród funkcjonalności b i c wystarczy wybrać jedną, która zostanie zaimplementowana.

b. Rysowanie kostki RGB

Kostka RGB powinna zostać narysowana w trójwymiarze,
Użytkownik powinien mieć możliwość obracania kostką,
Pokrycie kostki kolorami powinno odbywać się przy użyciu odpowiednich wzorów.
c. Rysowanie stożka HSV

Stożek HSV powinien zostać narysowany w trójwymiarze,
Użytkownik powinien mieć możliwość obracania stożkiem,
Pokrycie stożka kolorami powinno odbywać się przy użyciu odpowiednich wzorów,
Użytkownik powinien mieć możliwość obserwacji przekroju stożka: po wyborze odpowiedniego miejsca stożka powinien pojawić się jego przekrój (obok lub poprzez przecięcie stożka).

Projekt 4 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

a. Przekształcenia punktowe

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Wykonywanie następujących operacji na wczytanym obrazie:
Dodawanie (dowolnych podanych przez użytkownika wartości),
Odejmowanie (dowolnych podanych przez użytkownika wartości),
Mnożenie (przez dowolne podane przez użytkownika wartości),
Dzielenie (przez dowolne podane przez użytkownika wartości),
Zmiana jasności (o dowolny podany przez użytkownika poziom),
Przejście do skali szarości (na swa sposoby)
Przekształcenia punktowe należy zaimplementować samodzielnie, wykorzystanie bibliotek jest wykluczone.
b. Metody polepszania jakości obrazów

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Implementacja następujących filtrów  oraz zaprezentowanie ich działania na wczytanym obrazie:
Filtr wygładzający (uśredniający),
Filtr medianowy,
Filtr wykrywania krawędzi (sobel),
Filtr górnoprzepustowy wyostrzający,
Filtr rozmycie gaussowskie,
Splot maski dowolnego rozmiaru i dowolnych wartości elementów maski,
Spośród powyższych filtrów obowiązkowa jest implementacja filtrów 1 - 5. Za implementację filtru 6 przyznane zostaną dodatkowe punkty.

Filtry należy zaimplementować samodzielnie, wykorzystanie bibliotek jest wykluczone.

Projekt 5 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

a. Histogram

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Implementacja i zaprezentowanie działania normalizacji obrazu poprzez:
rozszerzenie histogramu,
wyrównanie (equalization) histogramu.
b. Binaryzacja

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Implementacja i zaprezentowanie działania binaryzacji z ustaleniem progów binaryzacji w następujący sposób:
Ręcznie przez użytkownika - użytkownik podaje próg bezpośrednio,
Procentowa selekcja czarnego (ang. Percent Black Selection),
Selekcja iteratywna średniej (ang. Mean Iterative Selection),
Selekcja entropii (ang. Entropy Selection),
Błąd Minimalny (ang. Minimum Error),
Metoda rozmytego błędu minimalnego (ang. Fuzzy Minimum Error).
Spośród powyższych sposobów binaryzacji konieczna jest implementacja sposobu 1 oraz dwóch wybranych spośród 2 - 6. Za implementację więcej niż dwóch sposobów binaryzacji wybranych spośród 2 - 6 przyznane zostaną dodatkowe punkty.

Projekt 6 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

Rysowanie krzywej Béziera,
Program może rysować krzywą Béziera o dowolnym stopniu; stopień rysowanej krzywej powinien zostać podany przez użytkownika,
Punkty charakterystyczne krzywej Béziera można podać podczas tworzenia za pomocą myszy lub przy pomocy pól tekstowych,
Punkty charakterystyczne krzywej Béziera można modyfikować za pomocą myszy (chwytanie i przeciąganie) oraz przy pomocy pól tekstowych,
Przy modyfikacji krzywej Béziera przy pomocy myszy zmiany na ekranie można obserwować na bieżąco - krzywa jest przeliczana w czasie rzeczywistym i zmiany są na bieżąco rysowane.
Filtry należy zaimplementować samodzielnie, wykorzystanie bibliotek jest wykluczone.

Projekt 7 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:
Definiowanie i rysowanie dowolnych figur - wielokątów przy użyciu myszy lub pól tekstowych,
Wykonywanie następujących przekształceń na stworzonych figurach:
Przesunięcie o zadany wektor,
Obrót względem zadanego punktu o zadany kąt,
Skalowanie względem zadanego punktu o zadany współczynnik,
Figury powinny być chwytane przy użyciu myszy
Wszystkie operacje powinny móc być wykonywane zarówno przy pomocy myszy, jak i za pomocą pól tekstowych:
Przesunięcie - przy użyciu myszy oraz po podaniu wektora i zatwierdzeniu,
Obrót - definiowanie punktu obrotu przy użyciu myszy oraz za pomocą pól tekstowych, wykonywanie obrotu przy użyciu myszy (chywanie i obracanie) oraz poprzez podanie i zatwierdzenie kąta obrotu w polu tekstowym,
Skalowanie - definiowanie punktu skalowania przy użyciu myszy oraz za pomocą pól tekstowych, wykonywanie skalowania przy użyciu myszy (chwytanie i skalowanie) oraz poprzez podanie i zatwierdzenie współczynnika skalowania w polu tekstowym,
Możliwość serializacji i deserializacji (zapisywanie i wczytywanie), tak aby za każdym uruchomieniem programu nie było konieczności rysowania figur od nowa.

Projekt 8 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Implementacja następujących filtrów morfologicznych oraz zaprezentowanie ich działania na wczytanym obrazie:
Dylatacja,
Erozja,
Otwarcie,
Domknięcie,
Hit-or-miss (pocienianie i pogrubianie),
Filtry należy zaimplementować samodzielnie, wykorzystanie bibliotek jest wykluczone.

Projekt 9 - wymagania
Aby projekt został oceniony maksymalnie, powinien posiadać następujące funkcjonalności:

Wczytanie obrazu (np. w analogiczny sam sposób, jaki był wykonany w ramach Projektu 2),
Obliczenie, ile procent wczytanego obrazu stanowią tereny zielone,
Wykonywanie obliczeń powinno odbywać się w sposób wydajny,
Wyniki obliczeń powinny być możliwie dokładne,
Wykorzystana metoda obliczeń jest w pełni dowolna,
Mile widziana możliwość parametryzacji programu, tak aby nie brał pod uwagę wyłącznie terenów zielonych, a także inne kolory / warunki wejściowe.