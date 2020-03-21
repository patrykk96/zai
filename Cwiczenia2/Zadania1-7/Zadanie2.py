tekst_zmiennej = "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został " \
                 "po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. " \
                 "Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie " \
                 "niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, " \
                 "zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum " \
                 "oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker"

imie = "Lukasz"
nazwisko = "Ziolkowski"
litera_1 = imie[2]
litera_2 = nazwisko[3]


def funkcja_zliczajaca_litery(litera_do_obliczenia, tekst):
    return tekst.count(litera_do_obliczenia)


print("W tekście jest %i liter %s oraz %i liter %s" % (funkcja_zliczajaca_litery(litera_1, tekst_zmiennej), litera_1,
                                                        funkcja_zliczajaca_litery(litera_2, tekst_zmiennej), litera_2))
