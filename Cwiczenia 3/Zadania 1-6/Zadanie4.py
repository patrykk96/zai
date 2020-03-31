def funkcja_obliczajaca_temp_z_celcjusza(temperature_type, temperatura):
    if temperatura < -273.15:
        'Bledna temperatura'
        return
    elif temperatura >= -275.15:
        if temperature_type == 'F':
            return (temperatura * 1.8) + 32
        elif temperature_type == 'R':  # Fahrenheit, Rankine, Kelvin
            return (temperatura + 273.15) * 1.8
        elif temperature_type == 'K':
            return temperatura + 273.15
        else:
            return 'Bledne oznaczenie jednostki temperatury'
    else:
        return "Blad programu"


print(funkcja_obliczajaca_temp_z_celcjusza('K', 15))
