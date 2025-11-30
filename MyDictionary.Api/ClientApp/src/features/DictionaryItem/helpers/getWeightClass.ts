const getWeightClass = (weight: number = 1, min: number, max: number) => {
  if (min === max) {
    return "weight-green";
  }

  const third = (max - min) / 3;

  if (weight <= min + third) {
    return "weight-green";
  } else if (weight <= min + third * 2) {
    return "weight-yellow";
  } else {
    return "weight-red";
  }
};

export default getWeightClass;