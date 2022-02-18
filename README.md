# Składnia pliku tekstowego

Definicja

`1:+ 2:- 5:- F2:S13`

spowoduje "dołożenie" wskaźnika W24 do sygnału podanego na semaforze F2, gdy na tym semaforze wyświetlony zostanie sygnał S13, a zwrotnice 1, 2 i 5 znajdą się w podanych położeniach. Jeżeli ułożenie zwrotnic i sygnałów w symulatorze nie ma odzwierciedlenia w żadnym zdefiniowanym przebiegu to wskaźnik W24 zostanie wygaszony.

Każda linia pliku tekstowego jest definicją osobnego przebiegu


# Gdzie umieścić program

Naprościej jest utworzyć w katalogu scenerii plik `przebiegi.txt` z definicjami i w tym samym katalogu umieścić `.exe`. Program sam wykryje obecność pliku tekstowego i go wczyta.