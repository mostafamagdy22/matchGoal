

  export const MatchStatusList = [
    { key: 'NotStarted', value: 0 },
    { key: 'InProgress', value: 1 },
    { key: 'Break', value: 2 },
    { key: 'Finished', value: 3 }
  ] as const;

export type MatchStatusListType = {
  key: string;
  value: number;
}[];

