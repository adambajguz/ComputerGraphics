
	function calculateThreshold(sliderVal){

		var src = _imageData.data;

		if (_methIndex == 0){

			_threshold = 255*sliderVal;
		}else if (_methIndex == 1){



		}else if (_methIndex == 2){



		}else if (_methIndex == 3){




			_threshold= _methThreeArr[_imgIndex];
			// var maxsum = _INT_MIN_VALUE,
			//     f = 0,
			//     Pt = 0;

			// var maxlow = _fhistogram[0],
			//     maxhigh = 0,
			//     Ht = 0,
			//     HT = 0;

			// for (i = 0; i < 256; i++){

			//     HT -= _fhistogram[i] * logN(_fhistogram[i], 2);
			// }

			// console.log("HT: ", HT);
			// for (i = 0; i < 256; i++){

			//     Pt += _fhistogram[i];
			//     maxlow = Math.max(maxlow, _fhistogram[i]);
			//     maxhigh = i < 255 ? _fhistogram[i + 1] : _fhistogram[i];

			//     for (var j = i + 2; j < 256; j++){

			//         if (_fhistogram[j] > maxhigh){

			//             maxhigh = _fhistogram[j];
			//         }
			//     }

			//     Ht -= (_fhistogram[i] * logN(_fhistogram[i], 2));
			//     f = Ht * logN(Pt, 2) / (HT * logN(maxlow, 2)) + (1 - Ht / HT) * logN(1 - Pt, 2) / logN(maxhigh, 2);
			//    // console.log("Ht: ", Ht);

			//     if (f > maxsum){
			//         console.log(f, maxsum);
			//         maxsum = f;
			//         _threshold = i;

			//     }
			// }


		}else if (_methIndex == 4){






			_threshold= _methFourArr[_imgIndex];
			// var minvalue = _INT_MAX_VALUE;
			// var  J, P1, P2, s1, s2, fv, u1, u2, Pi1, Pi2;
			// P1 = P2 = Pi1 = Pi2 = 0;
			// var v;
			// for (i = 0; i < 256; i++){
			//     v = _histogram[i];
			//     P2 += v;
			//     v *= i;
			//     Pi2 += v;

			// }
			// for (i = 0; i < 256; i++){

			//     v = _histogram[i];
			//     P1 += v;
			//     P2 -= v;
			//     v *= i;
			//     Pi1 += v;
			//     Pi2 -= v;
			//     u1 = P1 > 0 ? Pi1 / P1 : 0;
			//     u2 = P2 > 0 ? Pi2 / P2 : 0;
			//     s1 = 0;
			//    // console.log("v: ", v, "P2: ", P2, "i: ", i, "Pi2: ", Pi2, "P1: ", P1, "Pi1: ", Pi1, "u1: ", u1, "u2: ", u2);
			//     if (P1 > 0){

			//         for (var j = 0; j <= i; j++){

			//             fv = j - u1;
			//             s1 += fv * fv * _histogram[j];
			//         }
			//         s1 /= P1;
			//     }
			//     s2 = 0;

			//     if (P2 > 0){

			//         for (var j = i + 1; j < 256; j++){

			//             fv = j - u2;
			//             s2 += fv * fv * _histogram[j];
			//         }

			//         s2 /= P2;

			//     }
			//     console.log("J before: ", J, "i: ", i);
			//     J = 1 + 2 * ((P1 * log(s1) - log(P1)) + P2 * (log(s2) - log(P2) ) );
			//     console.log("J after: ", J, " minvalue: ", minvalue, "i: ", i);
			//     if (J < minvalue){
			//         console.log("i in thresh: ", i);
			//         _threshold = i;
			//         minvalue = J;
			//     }
			// }
		}else if (_methIndex == 5){

			var mu0, mu1, e, mine = _INT_MAX_VALUE;
			var max = 0, min = 255;
			for (i = 0; i < 256; i++){

				if (_histogram[i] > 0){

					if (i > max){
						max = i;
					}
					if (i < min){

						min = i;
					}
				}
			}

			var C = max - min;

			for (var t = 0; t < 255; t++){
				mu0 = 0;
				var c = 0;
				for (i = 0; i <= t; i++)
				{
					mu0 += i * _fhistogram[i];
					c += _fhistogram[i];
				}
				mu0 /= c;

				mu1 = 0;
				c = 0;
				for (i = t + 1; i < 256; i++)
				{
					mu1 += i * _fhistogram[i];
					c += _fhistogram[i];
				}
				mu1 /= c;

				e = 0;
				for (i = 0; i <= t; i++){

					e += Shannon(C / (C + Math.abs(i - mu0))) * _histogram[i];
				}

				for (i = t + 1; i < 256; i++){

					e += Shannon(C / (C + Math.abs(i - mu1))) * _histogram[i];
				}

				e /= src.length/4;
				if (e < mine){

					_threshold = t;
					mine = e;
				}
			}
		}


		_redLine.style.left= (_threshold/255)*170+2+"px";
		_sliderDisplay.innerHTML= "Threshold Level = "+ Math.floor(_threshold);
		_threshTarg.innerHTML = Math.floor(_threshold);
		// applyThreshold(_specimenImgData, _threshold);
	}