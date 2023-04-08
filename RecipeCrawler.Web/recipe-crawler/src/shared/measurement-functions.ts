import { Measurement } from "../models/shared/measurement.enum";

export default function useMeasurementFunctions() {
  function translateEnum(measurement: Measurement): string {
    let measurementType = "";
    switch (measurement) {
      case Measurement.Milliliters:
        measurementType = "Milliliter(s)";
        break;
      case Measurement.Milligrams:
        measurementType = "Milligram(s)";
        break;
      case Measurement.Grams:
        measurementType = "Gram(s)";
        break;
      case Measurement.Liters:
        measurementType = "Liter(s)";
        break;
      case Measurement.Kilograms:
        measurementType = "Kilogram(s)";
        break;
      case Measurement.TableSpoon:
        measurementType = "Tablespoon(s)";
        break;
      case Measurement.Teaspoon:
        measurementType = "Teaspoon(s)";
        break;
      case Measurement.Cup:
        measurementType = "Cup(s)";
        break;
      case Measurement.Pounds:
        measurementType = "Pound(s)";
        break;
      case Measurement.Ounces:
        measurementType = "Ounce(s)";
        break;
      case Measurement.FluidOunces:
        measurementType = "Fluid Ounce(s)";
        break;
      case Measurement.Pints:
        measurementType = "Pint(s)";
        break;
      case Measurement.Quarts:
        measurementType = "Quart(s)";
        break;
      case Measurement.Gallons:
        measurementType = "Gallons(s)";
        break;
    }
    return measurementType;
  }

  return {
    translateEnum,
  };
}
