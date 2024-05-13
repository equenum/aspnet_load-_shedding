import { check } from 'k6';
import { sleep } from 'k6';
import http from 'k6/http';

export const options = {
  scenarios: {
    open_model: {
      executor: 'constant-arrival-rate',
      rate: 400,
      timeUnit: '1s',
      duration: '30s',
      preAllocatedVUs: 400
    }
  }
};

export default () => {
  // high priority request
  const responseHigh = http.get('http://localhost:5000/api/public/Media/stream');

  // 0 - 3 second random sleep in between the calls
  sleep(Math.random() * 3);

  // low priority request
  const responseLow = http.get('http://localhost:5000/api/public/UserPreferences/lang');

  check(responseHigh, { 'high: 200': (res) => res.status == 200 });
  check(responseHigh, { 'high: 500': (res) => res.status == 500 });
  check(responseHigh, { 'high: 503': (res) => res.status == 503 });
  check(responseHigh, { 'high: 404': (res) => res.status == 404 });
  check(responseHigh, { 'high: 408': (res) => res.status == 408 });

  check(responseLow, { 'low: 200': (res) => res.status == 200 });
  check(responseLow, { 'low: 500': (res) => res.status == 500 });
  check(responseLow, { 'low: 503': (res) => res.status == 503 });
  check(responseLow, { 'low: 404': (res) => res.status == 404 });
  check(responseLow, { 'low: 408': (res) => res.status == 408 });
};
