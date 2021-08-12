import { QueryKey } from "react-query";
import {
  useInfinitePagination,
  UseInfinitePaginationOptions,
  UseInfinitePaginationReturnType,
} from "~/lib/hooks";
import { AudioView } from "../types";
import { getAudioFeedRequest } from "..";
import { useUser } from "~/features/user/hooks";

export const GET_AUDIO_FEED_QUERY_KEY: QueryKey = "feed";

export function useGetAudioFeed(
  options: UseInfinitePaginationOptions<AudioView> = {}
): UseInfinitePaginationReturnType<AudioView> {
  const { isLoggedIn } = useUser();
  return useInfinitePagination<AudioView>(
    GET_AUDIO_FEED_QUERY_KEY,
    (offset) => getAudioFeedRequest(offset),
    {
      enabled: isLoggedIn,
      ...options,
    }
  );
}
