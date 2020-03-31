tekst = "Przykladowy tekst"


def funkcja_analizujaca_tekst(data_text):
    lista_liter_tekstu = []

    slownik = {'length': len(data_text), 'letters': lista_liter_tekstu, 'big_letters': 'tekst_WIELIKI',
               'small_letters': 'tekst_malymi'}

    for i in data_text:
        lista_liter_tekstu.append(i)

    slownik['letters'] = lista_liter_tekstu

    slownik['big_letters'] = data_text.upper()

    slownik['small_letters'] = data_text.lower()

    return slownik


print(funkcja_analizujaca_tekst(tekst))
