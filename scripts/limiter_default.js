import { check } from 'k6';
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
  const response = http.get('http://localhost:5000/api/public/Media/stream');

  check(response, { 200: (res) => res.status == 200 });
  check(response, { 500: (res) => res.status == 500 });
  check(response, { 503: (res) => res.status == 503 });
  check(response, { 404: (res) => res.status == 404 });
  check(response, { 408: (res) => res.status == 408 });
};
