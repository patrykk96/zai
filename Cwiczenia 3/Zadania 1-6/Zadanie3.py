tekst_przykladowy = "Przykladowy tekst"


def usuwanie_liter_z_tekstu(text, letter):
    tekst_funkcji = [i for i in text if i != letter]
    return tekst_funkcji


print(usuwanie_liter_z_tekstu(tekst_przykladowy, 'k'))
